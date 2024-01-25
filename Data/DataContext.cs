using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Auth;
using System.Reflection.Emit;

namespace RestaurantApi.Data
{
    public class DataContext : IdentityDbContext<User1, Role, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public override DbSet<Role> Roles { get; set; }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
         //   builder.Entity<DishBasketDTO>().HasNoKey();
         //   builder.Entity<DishBasketDTO>().Ignore(Dis);

        }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<DishBasketDTO> dishBasketDTOs { get; set; }
        public DbSet<OrderDTO> OrderDTOs { get; set; }
        // public DbSet<User> Users { get; set; }
    }
}
