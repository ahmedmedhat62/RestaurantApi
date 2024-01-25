using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Interfaces;
using RestaurantApi.Migrations;
using RestaurantApi.Models;

namespace RestaurantApi.Repository
{
    public class DishesRepository : Idishes
    {
        private readonly DataContext _context;

        public DishesRepository(DataContext context )
        {
            _context = context;
            
        }

        /* public ICollection<Dishes> GetDishes(Category? category, bool? isVegan, Sort? sort)
         {
             var query = _context.Dishes.AsQueryable();

             if (category.HasValue)
             {
                 query = query.Where(x => x.category == category.Value);
             }

             if (isVegan.HasValue)
             {
                 query = query.Where(x => x.IsVegetarian == isVegan.Value);
             }

             if (sort.HasValue)
             {
                 switch (sort.Value)
                 {
                     case Sort.NameAsc:
                         query = query.OrderBy(x => x.Name);
                         break;
                     case Sort.NameDesc:
                         query = query.OrderByDescending(x => x.Name);
                         break;
                     case Sort.PriceAsc:
                         query = query.OrderBy(x => x.Price);
                         break;
                     case Sort.PriceDesc:
                         query = query.OrderByDescending(x => x.Price);
                         break;
                     case Sort.RatingAsc:
                         query = query.OrderBy(x => x.Rating);
                         break;
                     case Sort.RatingDesc:
                         query = query.OrderByDescending(x => x.Rating);
                         break;
                         // Add more sorting options as needed
                 }
             }

             return query.ToList();
         }*/

        public ICollection<Dishes> GetDishes(Category? category, bool? isVegan, Sort? sort, int? page)
        {
            const int pageSize = 5;
            int pageNumber = page ?? 1;

            IQueryable<Dishes> query = _context.Dishes;

            if (category.HasValue)
            {
                query = query.Where(x => x.category == category.Value);
            }

            if (isVegan.HasValue)
            {
                query = query.Where(x => x.IsVegetarian == isVegan.Value);
            }

            // Add sorting logic based on the 'sort' parameter (if provided)
            query = ApplySorting(query, sort);

            // Apply pagination
            var paginatedDishes = query.Skip((pageNumber - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList();
            foreach (var dish in paginatedDishes)
            {
                dish.Rating = CalculateAverageRating(dish.Id);
            }
            return paginatedDishes;
        }

        private IQueryable<Dishes> ApplySorting(IQueryable<Dishes> query, Sort? sort)
        {
            if (sort.HasValue)
            {
                switch (sort.Value)
                {
                    case Sort.NameAsc:
                        query = query.OrderBy(x => x.Name);
                        break;
                    case Sort.NameDesc:
                        query = query.OrderByDescending(x => x.Name);
                        break;
                    case Sort.PriceAsc:
                        query = query.OrderBy(x => x.Price);
                        break;
                    case Sort.PriceDesc:
                        query = query.OrderByDescending(x => x.Price);
                        break;
                    case Sort.RatingAsc:
                        query = query.OrderBy(x => x.Rating);
                        break;
                    case Sort.RatingDesc:
                        query = query.OrderByDescending(x => x.Rating);
                        break;
                        // Add more sorting options as needed
                }
            }

            return query;
        }

        public Dishes GetDishesByid(int dishId)
        {
            var dish = _context.Dishes.FirstOrDefault(x => x.Id == dishId);

            if (dish != null)
            {
                // Set Rating to AverageRating
                dish.Rating = CalculateAverageRating(dishId);
            }

            return dish;
        }

        // public async Task<Dishes> GetDishById1(int dishId)
        //{
        //  return await _context.Dishes.FindAsync(dishId);
        //}
        public async Task<bool> UserOrdered(int dishId, string userEmail)
        {
            return await _context.OrderDTOs
                .Where(o => o.OrderStatus == status.Delivered && o.useremail == userEmail)
                .AnyAsync(o => o.Baskets_Dishes.Any(b => b.dishId == dishId));
        }

        public async Task<bool> Rating(int id, int rating, string useremail)
        {
            // Check if the user has ordered the dish
            var userOrdered = await UserOrdered(id, useremail);

            if (!userOrdered)
            {
                return false; // User hasn't ordered the dish, cannot rate
            }

            // User has ordered the dish, find the dish by id
            var dish = _context.Dishes.FirstOrDefault(d => d.Id == id);

            if (dish == null)
            {
                return false; // Dish not found
            }

            // Update the dish's rating only if the user hasn't rated it before
            var existingRating = _context.Ratings.FirstOrDefault(r => r.DishId == id && r.UserEmail == useremail);

            if (existingRating != null)
            {
                // User has already rated the dish, update the existing rating
                existingRating.Rating = rating;
            }
            else
            {
                // User hasn't rated the dish before, create a new rating entry
                var newRating = new Models.Ratings
                {
                    DishId = id,
                    UserEmail = useremail,
                    Rating = rating
                };

                _context.Ratings.Add(newRating);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true; // Rating updated successfully
        }
        private double CalculateAverageRating(int dishId)
        {
            var ratings = _context.Ratings.Where(r => r.DishId == dishId).ToList();

            if (ratings.Count == 0)
            {
                return 0; // No ratings yet
            }

            // Calculate average rating
            var sumOfRatings = ratings.Sum(r => r.Rating);
            return (double)sumOfRatings / ratings.Count;
        }


    }
}
