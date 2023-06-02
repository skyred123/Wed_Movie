using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Wed_Movie.Data
{
    public class SeedData
    {
        public static async void SeedRoleUser( IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
            }
            var email = "admin@gmail.com";
            var password = "admin1";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                AppUser user = new()
                {
                    Email = email,
                    UserName = email
                };

                var result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                await roleManager.CreateAsync(role);
            }

            /*dbContext.Database.Migrate();
            if (dbContext.Roles.FirstOrDefault(e => e.Name == "admin") == null) {
                dbContext.Roles.Add(new IdentityRole("admin"));
            }
            if (dbContext.Roles.FirstOrDefault(e => e.Name == "user") == null)
            {
                dbContext.Roles.Add(new IdentityRole("user"));
            }
            if(dbContext.Users.FirstOrDefault(e=>e.Email== "admin@gmail.com") == null) {
                var email = "admin@gmail.com";
                var password = "admin1";
            }
            dbContext.SaveChanges();*/
        }
    }
}
