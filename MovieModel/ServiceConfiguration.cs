using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MovieModel.Config;
using MovieModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wed_Movie.Entities;

namespace MovieModel
{
    public class ServiceConfiguration
    {
        public static void ServiceAddTransient(IServiceCollection services)
        {
            services.AddTransient<PhanPhimService>();
            services.AddTransient<DienVienService>();
            services.AddTransient<HangService>();
            services.AddTransient<TheLoaiService>();
            services.AddTransient<PhimService>();
            services.AddTransient<TapPhimService>();
            services.AddTransient<TransactionService>();
        }
        public static async void CreateAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new ApplicationRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
            }
            var email = "admin@gmail.com";
            var password = "admin1";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                var user = new ApplicationUser()
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true,
                };

                var result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                var role = new ApplicationRole();
                role.Name = "User";
                await roleManager.CreateAsync(role);
            }
        }
    }
}
