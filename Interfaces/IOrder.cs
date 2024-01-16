using Microsoft.EntityFrameworkCore;
using RestaurantApi.Controllers;
using RestaurantApi.Data;
using RestaurantApi.Interfaces;
using RestaurantApi.Models;
using System;
using System.Threading.Tasks;

namespace RestaurantApi.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrderAsync(string userEmail, OrderCreateDto orderCreateDto);
        Task<OrderDTO> GetOrderAsync(Guid orderId);
        Task<List<OrderInfoDto>> GetOrders(string useremail);
        Task<OrderDTO> UpdateOrderStatusAsync(Guid orderId);
    }
}

namespace RestaurantApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dbContext;

        public OrderService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDTO> CreateOrderAsync(string userEmail, OrderCreateDto orderCreateDto)
        {
            try
            {
                var basket = await _dbContext.Baskets
                    .Include(b => b.Dishes)
                    .FirstOrDefaultAsync(b => b.Useremail == userEmail);

                if (basket == null || !basket.Dishes.Any())
                {
                    return null;
                }

                int totalPrice = basket.Dishes.Sum(d => d.Price * d.Amount);

                var orderDto = new OrderDTO
                {
                    useremail = userEmail,
                    Id = Guid.NewGuid(),
                    Address = orderCreateDto.address,
                    deliveryTime = orderCreateDto.delivery_Time,
                    orderTime = DateTime.UtcNow,
                    OrderStatus = status.InProcess,
                    price = totalPrice,
                    
                    Baskets_Dishes = basket.Dishes.ToList()
                };

                _dbContext.OrderDTOs.Add(orderDto);
                await _dbContext.SaveChangesAsync();

                basket.Dishes.Clear();
                await _dbContext.SaveChangesAsync();

                return orderDto;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return null;
            }
        }

        public async Task<OrderDTO> GetOrderAsync(Guid orderId)
        {
            try
            {
                var orderDto = await _dbContext.OrderDTOs
                    .Include(o => o.Baskets_Dishes) // Assuming OrderDTO has a navigation property named Baskets_Dishes
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                return orderDto;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return null;
            }

        }

        public async Task<List<OrderInfoDto>> GetOrders(string userEmail)
        {
            try
            {
                var orders = await _dbContext.OrderDTOs
                    .Where(o => o.useremail == userEmail)
                    .ToListAsync();

                var orderInfoDtos = orders.Select(MapOrderDtoToOrderInfoDto).ToList();
                return orderInfoDtos;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return new List<OrderInfoDto>();
            }
        }

        private OrderInfoDto MapOrderDtoToOrderInfoDto(OrderDTO orderDto)
        {
            return new OrderInfoDto
            {
                Id = orderDto.Id,
                deliveryTime = orderDto.deliveryTime,
                orderTime = orderDto.orderTime,
                OrderStatus = orderDto.OrderStatus,
                price = orderDto.price
                // Map other properties as needed
            };
        }

        public async Task<OrderDTO> UpdateOrderStatusAsync(Guid orderId)
        {
            try
            {
                var orderDto = await _dbContext.OrderDTOs
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                if (orderDto == null)
                {
                    return null;
                }

                orderDto.OrderStatus = status.Delivered;

                await _dbContext.SaveChangesAsync();

                return orderDto;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return null;
            }
        }

    }
}