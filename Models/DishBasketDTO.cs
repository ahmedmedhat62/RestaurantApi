using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class DishBasketDTO
    {

        [Key]
        public int Id { get; set; }

        

        public string Name { get; set; }

        public int Price { get; set; }

        public int TotalPrice { get; set; }

        public int Amount { get; set; }

        public string Image { get; set; }

        public DishBasketDTO(Dishes dish)
        {
            Id = dish.Id;
            Name = dish.Name;
            Price = dish.Price;
            // You might want to calculate TotalPrice and set it based on the logic you have
            Amount = 1; // You can set an initial amount
            TotalPrice = dish.Price * Amount;
           
            Image = dish.Photo;
        }

    }
}
