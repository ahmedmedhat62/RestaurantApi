
using System.Text.Json.Serialization;

namespace RestaurantApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Category
    {
        Wok,
        Pizza,
        Soup,
        Dessert,
        Drink,
        Top_Tier
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sort
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,
        RatingAsc,
        RatingDesc
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male,
        Female

    }
}
