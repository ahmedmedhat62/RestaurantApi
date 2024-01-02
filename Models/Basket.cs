using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        public List<DishBasketDTO> Dishes { get; set; } = new List<DishBasketDTO>();
        public int dish_id { get; set; }
        public string Useremail { get; set; }



    }

}
