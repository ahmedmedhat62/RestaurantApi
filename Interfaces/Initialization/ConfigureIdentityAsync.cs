using Microsoft.AspNetCore.Identity;
using RestaurantApi.Auth;
using RestaurantApi.Data;
using RestaurantApi.Models;
namespace WebApplication2.Services.Initialization
{
    public static class ConfigureIdentity
    {
        public static async Task ConfigureIdentityAsync(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User1>>();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
            
            var config = app.Configuration.GetSection("AdminCredentials");
            // Creation of role
            var adminRole = await roleManager.FindByNameAsync(Roles.Admin);
            if (adminRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new Role
                {
                    Name = Roles.Admin
                });
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create role {Roles.Admin}");
                }

                adminRole = await roleManager.FindByNameAsync(Roles.Admin);
            }

            // Creation of user

            /*var adminUser = await userManager.FindByEmailAsync(config["Email"]);
            if (adminUser == null)
            {
                var userResult = await userManager.CreateAsync(new User1
                {
                    id = config["8F9C358E-05F7-4588-8860-08DC00F7CC6Aa"]  ,
                    UserName = config["Email"],
                    Email = config["Email"],
                    Name = "Admin",
                    Address = "Address a7a",
                    PhoneNumber = "01028199546",
                    Gender = 0,
                    BirthDate = new DateTime(2000, 1, 1)
                }, config["Password"]);

                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create user with email={config["Email"]}");
                }
                adminUser = await userManager.FindByEmailAsync(config["Email"]);
            }*/

           /* if (!await userManager.IsInRoleAsync(adminUser, adminRole.Name))
            {
                await userManager.AddToRoleAsync(adminUser, adminRole.Name);
            }*/
        }
    }
}
