using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Interfaces;
using RestaurantApi.Models;

namespace RestaurantApi.Controllers
{
    [Route("api/Dishes")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly Idishes _dishes;

        public DishesController(Idishes dishes)
        {
            _dishes = dishes;
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

      
    }
}
