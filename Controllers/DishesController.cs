using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Interfaces;
using RestaurantApi.Models;
using System.Security.Claims;

namespace RestaurantApi.Controllers
{
    [Route("api/Dishes")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly Idishes _dishes;
        private readonly DataContext _context;

        public DishesController(Idishes dishes, DataContext context )
        {
            _dishes = dishes;
            _context = context;
        }
        /// <summary>
        /// Get a list of dishes (menu)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dishes>))]
        public IActionResult GetDishes( int? PageNumber  , Category? category = null, bool? isvegan = null, Sort? sort = null)
        {
            var dishes = _dishes.GetDishes(category, isvegan, sort , PageNumber );
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(dishes);
        }
        /// <summary>
        /// Get information about concrete dish
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Dishes))]
        [ProducesResponseType(404)]
        public IActionResult GetDishById(int id )
        {
            var dish = _dishes.GetDishesByid(id);
            if (dish == null)
            {
                return NotFound();
            }
            return Ok(dish);
        }
        /// <summary>
        /// Checks if user is able to set rating of the dish
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/rating/check")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> UserOrdered(int id)
        {
            // Retrieve user email from claims
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            // Check if the user is authenticated
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(); // Return 401 Unauthorized status
            }

            // Use the existing UserOrdered function
            var userOrdered = await _dishes.UserOrdered(id, userEmail);

            return Ok(userOrdered);
        }
        /// <summary>
        /// Set a rating for a dish
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/rating")]
        [Authorize] // Require authentication to access this endpoint
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(401)] // Unauthorized
        [ProducesResponseType(400, Type = typeof(string))] // Bad Request with error message
        public async Task<IActionResult> RateDish(int id, int rate)
        {
            // Retrieve user email from claims
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            // Check if the user is authenticated
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(); // Return 401 Unauthorized status
            }

            // Use the Rating function from the dishes repository
            var ratingSuccess = await _dishes.Rating(id, rate, userEmail);

            if (!ratingSuccess)
            {
                return BadRequest("Unable to rate the dish. Make sure you have ordered it.");
            }

            return Ok(true);
        }


    }
}
