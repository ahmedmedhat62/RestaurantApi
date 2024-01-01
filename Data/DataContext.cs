using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Auth;

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
        }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        // public DbSet<User> Users { get; set; }
    }
}
