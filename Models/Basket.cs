using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }


        /*
                public Guid UserId { get; set; }

                public List<DishBasketDTO> dishes { get; set; }*/
        public List<Dishes> Dishes { get; set; } = new List<Dishes>();
        public int dish_id { get; set; }
        public string Useremail { get; set; }



    }
}
