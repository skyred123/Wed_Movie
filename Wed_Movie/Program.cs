/*using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Hosting.Internal;
using Wed_Movie.Data;
using Wed_Movie.DI;
using Wed_Movie.Functions;
using Imgix;
using Wed_Movie.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.User.RequireUniqueEmail = false;
        });

        builder.Services.AddControllersWithViews();

        builder.Services.AddMvc();

        builder.Services.AddScoped<IUploadFile,Upload>();


        var app = builder.Build();

        app.MapControllers();

        app.MapAreaControllerRoute(
            name: "Admin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.MapDefaultControllerRoute();

        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        SeedData.SeedRoleUser(app.Services.CreateScope().ServiceProvider);
        app.Run();
    }
}*/

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Wed_Movie;

namespace YourNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}