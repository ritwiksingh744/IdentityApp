using IdentityApp.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Data
{ 
    public static class DbSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // 1. Seed Roles
            string[] roles = { "Admin", "Manager", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2. Seed Users

            // Admin
            if (await userManager.FindByEmailAsync("admin@yopmail.com") == null)
            {
                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@yopmail.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            // 2 Managers
            for (int i = 1; i <= 2; i++)
            {
                string email = $"manager{i}@yopmail.com";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var manager = new User
                    {
                        UserName = $"manager{i}",
                        Email = email,
                        FirstName = $"Manager{i}",
                        LastName = "User",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(manager, $"Manager{i}@123");
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(manager, "Manager");
                }
            }

            // 3 Regular Users
            for (int i = 1; i <= 3; i++)
            {
                string email = $"user{i}@yopmail.com";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new User
                    {
                        UserName = $"user{i}",
                        Email = email,
                        FirstName = $"User{i}",
                        LastName = "Person",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, $"User{i}@123");
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }

}
