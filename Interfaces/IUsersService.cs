using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantApi.Auth;
using RestaurantApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace RestaurantApi.Interfaces
{
    public interface IUsersService
    {
        Task<string> Register(UserRegister model);
        Task<UserDTO> GetProfile(string email);
        Task<string> Login(LoginModel model);
        Task UpdateProfile(string email, UserEdit model);
        Task Logout(ClaimsPrincipal user, string userEmail);
    }

    public class UsersService : IUsersService
    {
        private readonly UserManager<User1> _userManager;
        private readonly JwtBearerTokenSettings _bearerTokenSettings;
        private readonly SignInManager<User1> _signInManager; // Add SignInManager

        public UsersService(UserManager<User1> userManager, IOptions<JwtBearerTokenSettings> jwtTokenOptions, SignInManager<User1> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _bearerTokenSettings = jwtTokenOptions.Value;
        }

        public async Task<UserDTO> GetProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with email {email} does not found");
            }

            return new UserDTO
            {
                id = user.Id,
                Email = user.Email,
                Birthdate = user.BirthDate,
                FullName = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Gender = user.Gender
            };
        }

        public async Task<string> Login(LoginModel model)
        {
            var user = await ValidateUser(model);
            if (user == null)
            {
                throw new InvalidOperationException("Login failed");
            }

            return GenerateToken(user, await _userManager.IsInRoleAsync(user, Roles.Admin));
        }
        public async Task Logout(ClaimsPrincipal user, string userEmail)
        {
            
            // Sign the user out (clear authentication cookie)
            await _signInManager.SignOutAsync();

            // If user is authenticated and has an email claim, remove it
            if (!string.IsNullOrEmpty(userEmail))
            {
                var currentUser = await _userManager.FindByEmailAsync(userEmail);
                if (currentUser != null)
                {
                    // Remove the email claim
                    await _userManager.RemoveClaimAsync(currentUser, new Claim(ClaimTypes.Email, userEmail));
                }
            }
        }

        public async Task UpdateProfile(string email, UserEdit model)
        {
            var user =  await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

          

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {user} does not found");
            }

            // Update user properties
            user.Name = model.FullName;
            user.Address = model.Address;
            user.BirthDate = model.Birthdate;
            user.PhoneNumber = model.PhoneNumber;
            user.Gender = model.Gender;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user profile");
            }
        }

        public async Task<string> Register(UserRegister model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("User with the same email already exists");
            }

            /*if (!IsValidPhoneNumber(model.PhoneNumber))
            {
                throw new ArgumentException("Invalid phone number format");
            }*/

            var identityUser = new User1
            {
                Name = model.FullName,
                Address = model.Address,
                Email = model.Email,
                BirthDate = model.Birthdate,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Some errors during creating user");
            }

            // If user registration is successful, generate and return the token
            return GenerateToken(identityUser, await _userManager.IsInRoleAsync(identityUser, Roles.Admin));
        }

        private async Task<User1> ValidateUser(LoginModel credentials)
        {
            var identityUser = await _userManager.FindByEmailAsync(credentials.Email);
            if (identityUser != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash,
                    credentials.Password);
                return result == PasswordVerificationResult.Success ? identityUser : null;
            }

            return null;
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Use a regular expression to check if the phone number matches the desired format
            // Adjust the regular expression according to your specific requirements
            var regex = new Regex(@"^\+\d{1,2}\s\(\d{3}\)\s\d{3}-\d{2}-\d{2}-\d{2}$");
            return regex.IsMatch(phoneNumber);
        }

        private string GenerateToken(User1 user, bool isAdmin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_bearerTokenSettings.SecretKey);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, isAdmin ? Roles.Admin : Roles.Admin)
                }),
                Expires = DateTime.UtcNow.AddSeconds(_bearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _bearerTokenSettings.Audience,
                Issuer = _bearerTokenSettings.Issuer,
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
