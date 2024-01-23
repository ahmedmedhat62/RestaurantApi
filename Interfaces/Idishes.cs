using RestaurantApi.Models;

namespace RestaurantApi.Interfaces
{
    public interface Idishes
    {
        ICollection<Dishes> GetDishes(Category? category , bool? isvegan , Sort? sort , int? pagenumber );
        Dishes GetDishesByid(int dishId );
        Task<Dishes> GetDishById1(int dishId);
        Task<bool> UserOrdered(int dishId , string useremail);

        // ICollection<Dishes> GetDishesByCategory(Category category);
    }
}
