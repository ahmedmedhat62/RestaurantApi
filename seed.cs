/*using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApi.Data;
using RestaurantApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApi
{
    public class Seed
    {
        public static void seeding(IApplicationBuilder applicationBuilder) 
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                context.Database.EnsureCreated();
                if (!context.Dishes.Any()) 
                {
                    context.Dishes.AddRange(new List<Dishes>() 
                    {
                        new Dishes()
                        {   
                            Name = "dish1",
                            category = Category.Drink,
                            IsVegetarian = true,
                            Photo = "img1",
                            Price = 200 ,
                            Rating = 1 ,
                            Description = "a7a neek omk el 3ae2a"
                            
                        },
                        new Dishes()
                        {
                            Name = "dish2",
                            category = Category.Wok,
                            IsVegetarian = true,
                            Photo = "img2",
                            Price = 300 ,
                            Rating = 2 ,
                            Description = " a7a neek 5altk el sharmota"

                        },
                        new Dishes()
                        {
                            Name = "dish3",
                           category= Category.Pizza,
                            IsVegetarian = false,
                            Photo = "img3",
                            Price = 500 ,
                            Rating = 5 ,
                            Description = "a7a neek 3ady"
                            

                        },

                    });
                    context.SaveChanges();
                }
            }
        
        
        
        
        
        }
   

    }
}*/
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApi.Data;
using RestaurantApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApi
{
    public class Seed
    {
        public static void seeding(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                context.Database.EnsureCreated();
                if (!context.Dishes.Any())
                {
                    context.Dishes.AddRange(new List<Dishes>()
                    {
                        new Dishes
                        {
                            Name = "Stir-Fried Noodles",
                            category = Category.Wok,
                            IsVegetarian = true,
                            Photo = "stir_fried_noodles.jpg",
                            Price = 300,
                            Rating = 2,
                            Description = "Wok-tossed noodles with mixed vegetables and soy sauce."
                        },
                        new Dishes
                        {
                            Name = "Pepperoni Pizza",
                            category = Category.Pizza,
                            IsVegetarian = false,
                            Photo = "pepperoni_pizza.jpg",
                            Price = 450,
                            Rating = 3.1,
                            Description = "Classic pizza topped with pepperoni, tomato sauce, and melted cheese."
                        },
                        new Dishes
                        {
                            Name = "Chicken Noodle Soup",
                            category = Category.Soup,
                            IsVegetarian = false,
                            Photo = "chicken_noodle_soup.jpg",
                            Price = 200,
                            Rating = 3.5,
                            Description = "Hearty chicken noodle soup with vegetables and savory broth."
                        },
                        new Dishes
                        {
                            Name = "Chocolate Cake",
                            category = Category.Dessert,
                            IsVegetarian = true,
                            Photo = "chocolate_cake.jpg",
                            Price = 350,
                            Rating = 4.25,
                            Description = "Decadent chocolate cake topped with rich chocolate ganache."
                        },
                        new Dishes
                        {
                            Name = "Mango Smoothie",
                            category = Category.Drink,
                            IsVegetarian = true,
                            Photo = "mango_smoothie.jpg",
                            Price = 150,
                            Rating = 4.2,
                            Description = "Refreshing mango smoothie with blended mango, yogurt, and ice."
                        },
                         new Dishes
                        {
                            Name = "Margarita Pizza",
                            category = Category.Pizza,
                            IsVegetarian = true,
                            Photo = "Mar_Pizza.jpg",
                            Price = 350,
                            Rating = 4.25,
                            Description = "Pizza with cheese"
                        },
                          new Dishes
                        {
                            Name = "Chicken Alfredo Pasta",
                            category = Category.Wok,
                            IsVegetarian = false,
                            Photo = "chicken_alfredo_pasta.jpg",
                            Price = 400,
                            Rating = 4.2,
                            Description = "Creamy chicken alfredo pasta with fettuccine noodles"
                        },
                          new Dishes
                          {
                            Name = "Tom Yum Soup",
                            category = Category.Soup,
                            IsVegetarian = false,
                            Photo = "tom_yum_soup.jpg",
                            Price = 700,
                            Rating = 3.8,
                            Description = "Spicy and sour Thai Tom Yum soup with shrimp and vegetables"
                          },
                          new Dishes
                          {
                            Name = "Chocolate Lava Cake",
                            category = Category.Dessert,
                            IsVegetarian = true,
                            Photo = "chocolate_lava_cake.jpg",
                            Price = 499,
                            Rating = 1.7,
                            Description = "Warm and gooey chocolate lava cake with a melting center"
                          },
                          new Dishes
                          {
                            Name = "Mango Tango Smoothie",
                            category = Category.Drink,
                            IsVegetarian = true,
                            Photo = "mango_tango_smoothie.jpg",
                            Price = 550,
                            Rating = 4.0,
                            Description = "Refreshing mango and pineapple smoothie with a hint of mint"
                          },
                          new Dishes
                          {
                            Name = "Grilled Salmon",
                            category = Category.Wok,
                            IsVegetarian = false,
                            Photo = "grilled_salmon.jpg",
                            Price = 640,
                            Rating = 4.6,
                            Description = "Healthy grilled salmon with lemon and herbs"
                          },
                          new Dishes
                          {
                            Name = "Caprese Salad",
                            category = Category.Dessert,
                            IsVegetarian = true,
                            Photo = "caprese_salad.jpg",
                            Price = 170,
                            Rating = 2.3,
                            Description = "Fresh Caprese salad with tomatoes, mozzarella, and balsamic glaze"
                          },
                          new Dishes
                          {
                            Name = "Matcha Latte",
                            category = Category.Drink,
                            IsVegetarian = true,
                            Photo = "matcha_latte.jpg",
                            Price = 240,
                            Rating = 3.1,
                            Description = "Creamy matcha latte with a touch of sweetness"
                          },
                          new Dishes
                          {
                            Name = "Mahshy",
                            category = Category.Top_Tier,
                            IsVegetarian = true,
                            Photo = "https://sugarandgarlic.com/wp-content/uploads/2020/10/mahshy-11-of-14.jpg",
                            Price = 10000,
                            Rating = 5,
                            Description = "Beauty in the Form of a dish"
                          }
                          
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
