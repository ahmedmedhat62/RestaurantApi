using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Auth;
using RestaurantApi.Data;
using RestaurantApi.Interfaces;
using RestaurantApi.Models;

namespace RestaurantApi.Interfaces
{
   public interface IBasketService
{
        // Add a dish to the basket for a specific user
        //Task<bool> AddDishToBasketAsync(Guid userId, int dishId);
        Task<bool> AddStringToBasketAsync(string userEmail,int id);
        Task<List<DishBasketDTO>> GetDishesInBasketAsync(string userEmail);

    }
}

public class BasketService : IBasketService
{
    private readonly DataContext _dbContext;

    public BasketService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    /*public async Task<bool> AddDishToBasketAsync(Guid userId, int dishId)
    {
        try
        {
            // Check if the user already has a basket
            var basket = await _dbContext.Baskets
                .Include(b => b.dishes)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            // If the user doesn't have a basket, create one
            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = userId,
                    dishes = new List<DishBasketDTO>()
                };
                _dbContext.Baskets.Add(basket);
            }

            // Check if the dish is already in the basket
            var existingDish = basket.dishes.FirstOrDefault(d => d.Id == dishId);

            if (existingDish != null)
            {
                // If the dish is already in the basket, increment the amount
                existingDish.Amount++;
            }
            else
            {
                // If the dish is not in the basket, add it
                var dish = await _dbContext.Dishes.FindAsync(dishId);

                if (dish != null)
                {
                    basket.dishes.Add(new DishBasketDTO
                    {
                        Id = dish.Id,
                        Name = dish.Name,
                        Price = dish.Price,
                        TotalPrice = dish.Price, // You might want to calculate this based on amount
                        Amount = 1, // Initial amount is 1
                        Image = dish.Photo
                    });
                }
                else
                {
                    // Handle the case where the dish with the specified ID is not found
                    return false;
                }
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return false;
        }
    }*/
    public async Task<bool> AddStringToBasketAsync(string userEmail, int id)
    {
        try
        {
            // Check if the user already has a basket
            var basket = await _dbContext.Baskets
                .Include(b => b.Dishes)
                .FirstOrDefaultAsync(b => b.Useremail == userEmail);

            // If the user doesn't have a basket, create one
            if (basket == null)
            {
                basket = new Basket
                {
                    Useremail = userEmail,
                    // Initialize any other properties of the basket here
                };
                _dbContext.Baskets.Add(basket);
            }

            // Add the dish to the basket
            var dish = await _dbContext.Dishes.FindAsync(id);
            if (dish != null && !basket.Dishes.Contains(dish))
            {
                basket.Dishes.Add(dish);
            }


            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return false;
        }
    }

    public async Task<List<DishBasketDTO>> GetDishesInBasketAsync(string userEmail)
    {
        try
        {
            var basket = await _dbContext.Baskets
                .Include(b => b.Dishes)
                .FirstOrDefaultAsync(b => b.Useremail == userEmail);

            var dishBasketDTOs = basket?.Dishes.Select(dish => new DishBasketDTO(dish)).ToList() ?? new List<DishBasketDTO>();

            return dishBasketDTOs;
        }

        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return new List<DishBasketDTO>();
        }
    }

}