using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Interfaces;
using RestaurantApi.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        private readonly IBasketService _basketService;

        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }
        /// <summary>
        /// Creating the order from dishes in basket
        /// </summary>
        /// <returns></returns>
        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User email not found in claims.");
                }

                // Get the dishes in the user's cart
               // var dishesInCart = await _basketService.GetDishesInBasketAsync(userEmail);

                // Create an order using the provided orderCreateDto and the dishes in the cart
                var orderId = await _orderService.CreateOrderAsync(userEmail, orderCreateDto);

                return Ok($"Order created successfully with ID: {orderId}");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        /// Get information about concrete order
        /// </summary>
        /// <returns></returns>
        [HttpGet("order/{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(id);

                if (order == null)
                {
                    return NotFound("Order not found. Please check the order ID.");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        /// Get a list of orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("order")]
        public async Task<IActionResult> GetUserOrders()
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User email not found in claims.");
                }

                var orders = await _orderService.GetOrders(userEmail);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        /// Confirm order delivery
        /// </summary>
        /// <returns></returns>
        [HttpPost("order/{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderStatusAsync(id);

                if (updatedOrder == null)
                {
                    return NotFound("Order not found. Please check the order ID.");
                }

                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}