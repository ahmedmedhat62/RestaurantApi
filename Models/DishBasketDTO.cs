using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class DishBasketDTO
    {

        [Key]
        public int Id { get; set; }

        
        public int dishId { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int TotalPrice { get; set; }

        public int Amount { get; set; }

        public string Image { get; set; }

        public DishBasketDTO(int id, string name, int price, int amount, string image)
        {
            Id = id;
            Name = name;
            Price = price;
           
            Amount = amount;
            TotalPrice = price * amount;
            Image = image;
        }

    }
}
