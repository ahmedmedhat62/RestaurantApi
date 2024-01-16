using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantApi.Models
{
    public class OrderDTO
    {
        [Key]
        public Guid Id { get; set; }

        public string Address { get; set; }

        public DateTime deliveryTime { get; set; }

        public DateTime orderTime { get; set; }

        public status OrderStatus { get; set; }

        public int price { get; set; }

        public string useremail { get; set; }

        public List<DishBasketDTO> Baskets_Dishes { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum status
    {
        InProcess, Delivered
    }
}
