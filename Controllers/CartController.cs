using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantApi.Interfaces;
using RestaurantApi.Models;
using System.Security.Claims;
using System.Threading.Tasks;


[Authorize]
[ApiController]
[Route("api/basket")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPost("add-to-basket")]
    public async Task<IActionResult> AddToBasket(int id)
    {
        try
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User email not found in claims.");
            }

            var result = await _basketService.AddStringToBasketAsync(userEmail, id);

            if (result)
            {
                return Ok("Dish added to the basket successfully.");
            }
            else
            {
                return BadRequest("Failed to add dish to the basket. Please check the dish ID.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("get-dishes-in-basket")]
    public async Task<IActionResult> GetDishesInBasket()
    {
        try
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User email not found in claims.");
            }

            var dishes = await _basketService.GetDishesInBasketAsync(userEmail);

            return Ok(dishes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}