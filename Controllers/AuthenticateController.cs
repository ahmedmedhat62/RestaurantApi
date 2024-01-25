using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Interfaces;
using RestaurantApi.Models;
using Microsoft.AspNetCore.Identity;
using RestaurantApi.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RestaurantApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        /// <summary>
        /// Log in to the system
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                // Call the Login method in the service, which returns the token upon successful login
                var token = await _usersService.Login(model);

                // Return the token in the response
                return Ok(new { Token = token });
            }
            catch (InvalidOperationException ex)
            {
                // Handle login failure
                // Write logs if needed
                return BadRequest("Invalid login credentials");
            }
            catch (Exception ex)
            {
                // Handle other exceptions and log
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Register new user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Post(UserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Call the Register method in the service, which returns the token upon successful registration
                var token = await _usersService.Register(model);

                // Return the token in the response
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Handle exceptions and log
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Edit user profile
        /// </summary>
        /// <returns></returns>
        [HttpPut("Profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UserEdit model)
        {
            try
            {
                var emailClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                await _usersService.UpdateProfile(emailClaim.Value, model);
                return Ok("Profile updated successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions and log
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> Get()
        {
            try
            {
                var emailClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                return await _usersService.GetProfile(emailClaim.Value);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // logs
            }

            return BadRequest();
        }
        /// <summary>
        /// Log out to system user
        /// </summary>
        /// <returns></returns>

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                // Retrieve user email from claims
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                // Remove the email claim when logging out
                _usersService.Logout(User, userEmail);

                // Revoke the refresh token (if applicable)
                // You need to implement the logic for revoking refresh tokens

                // Set the token lifetime to zero to expire the access token immediately
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddSeconds(0),
                    IsPersistent = false
                };

                // Sign out the user
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, authProperties);

                return Ok("Logout successful");
            }
            catch (Exception ex)
            {
                // Handle exceptions and log
                return StatusCode(500, "Internal Server Error");
            }
        }


    }

}