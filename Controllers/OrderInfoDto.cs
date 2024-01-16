using RestaurantApi.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Controllers
{
    public class OrderInfoDto
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime deliveryTime { get; set; }

        public DateTime orderTime { get; set; }

        public status OrderStatus { get; set; }

        public int price { get; set; }

        

    }
}
