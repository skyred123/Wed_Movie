using Microsoft.AspNetCore.Identity;
using Wed_Movie.DI;
using Wed_Movie.Functions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using MediaToolkit;
using JavaScriptEngineSwitcher.V8;
using Newtonsoft.Json;
using FFMpegCore;
using Microsoft.EntityFrameworkCore;
using MovieModel.Config;
using MovieModel;
using Wed_Movie.SendMail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Wed_Movie.Entities;

namespace Wed_Movie
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString"));
                options.UseLoggerFactory(LoggerFactory.Create(log =>
                {
                    log.AddConsole();
                }));
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });

            GlobalFFOptions.Configure(new FFOptions { BinaryFolder = "./ffmpeg/bin", TemporaryFilesFolder = "/tmp" });
            //install microsoft.extensions.configuration
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 1024 * 1024 * 1024; // 1GB expressed in bytes
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
            services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName).AddV8();
            services.AddMediaToolkit("ffmpeg.exe");

            services.AddDbContext<ApplicationDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                // Cấu hình JsonSerializerSettings ở đây
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                // Thêm các cấu hình khác nếu cần thiết
            });
            services.AddMvc();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "852771441778-apv48vmro2e5tncnmnuefk9ehneinn65.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });


            services.AddScoped<IUploadFile, Upload>();

            var emailConfig = Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            ServiceConfiguration.ServiceAddTransient(services);
            ServiceConfiguration.CreateAdmin(services.BuildServiceProvider());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

