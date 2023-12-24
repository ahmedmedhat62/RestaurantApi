namespace RestaurantApi.Models
{
    public class Dishes
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public bool IsVegetarian { get; set; }

        public string Photo { get; set; }
                                        
        public double Rating { get; set; }

        public Category category { get; set; }
        
    }
}
