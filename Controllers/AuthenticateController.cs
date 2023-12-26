using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Interfaces;
using RestaurantApi.Models;
using Microsoft.AspNetCore.Identity;
using RestaurantApi.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                return Ok(await _usersService.Login(model));
            }
            catch (InvalidOperationException ex)
            {
                // Write logs
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return BadRequest();
        }
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
                await _usersService.Register(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return BadRequest();
        }
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

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _usersService.Logout();
                return Ok("Logout successful");
            }
            catch (Exception ex)
            {
                // Handle exceptions and log
                return StatusCode(500);
            }
        }


    }

}