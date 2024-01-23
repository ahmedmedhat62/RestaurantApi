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

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Dishes))]
        [ProducesResponseType(404)]
        public IActionResult GetDishById(int id , Category category)
        {
            var dish = _dishes.GetDishesByid(id);
            if (dish == null)
            {
                return NotFound();
            }
            return Ok(dish);
        }
        [HttpGet("{id}/rating/check")]
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


    }
}
