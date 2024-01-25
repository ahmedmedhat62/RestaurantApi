using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class Ratings
    {
        [Key]
        public Guid Id { get; set; }

        public int DishId { get; set; }

        public string UserEmail { get; set; }

        public int Rating { get; set; }
    }
}
