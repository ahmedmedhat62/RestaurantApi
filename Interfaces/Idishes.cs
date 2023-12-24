﻿using RestaurantApi.Models;

namespace RestaurantApi.Interfaces
{
    public interface Idishes
    {
        ICollection<Dishes> GetDishes(Category? category , bool? isvegan , Sort? sort , int? pagenumber );
        Dishes GetDishesByid(int dishId );
        
       // ICollection<Dishes> GetDishesByCategory(Category category);
    }
}
