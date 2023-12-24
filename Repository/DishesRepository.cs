using RestaurantApi.Data;
using RestaurantApi.Interfaces;
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
            return _context.Dishes.FirstOrDefault(x => x.Id == dishId);
        }
       

    }
}
