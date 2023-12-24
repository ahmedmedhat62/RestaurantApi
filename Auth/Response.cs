namespace RestaurantApi.Auth
{
    public class Response
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
