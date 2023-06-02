using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using Wed_Movie.Models;

namespace Wed_Movie.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Hang> Hangs { get; set; }
        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<Phim> Phims { get; set; }
        public DbSet<PhanPhim> PhanPhims { get; set; }
        public DbSet<TapPhim> TapPhims { get; set; }
        public DbSet<DienVien> DienViens { get; set; }
        public DbSet<CT_TheLoai> CT_TheLoais { get; set; }
        public DbSet<CT_Hang> CT_Hangs { get; set; }

        
        public DbSet<CT_DienVien> CT_DienViens { get; set; }

        public AppDbContext()
        : base(getApplicationDbContext())
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<CT_TheLoai>().HasKey(e => new { e.IdPhanPhim, e.IdTheLoai });
            builder.Entity<CT_Hang>().HasKey(e => new { e.IdPhanPhim, e.IdHang });
            builder.Entity<CT_DienVien>().HasKey(e => new { e.IdDienVien, e.IdPhanPhim});
        }
        public static AppDbContext Create()
        {
            var dbContext = new AppDbContext();
            dbContext.Hangs.Include(e => e.CT_Hang).Load();
            dbContext.TheLoais.Include(e => e.CT_TheLoais).Load();
            dbContext.Phims.Include(e => e.PhanPhim).Load();
            dbContext.PhanPhims.Include(e => e.Phim).Include(e => e.CT_DienVien).Include(e => e.CT_TheLoais).Include(e => e.CT_Hangs).Include(e => e.TapPhim).Load();
            dbContext.TapPhims.Include(e => e.PhanPhim);
            dbContext.DienViens.Include(e => e.CT_DienViens).Load();
            dbContext.CT_DienViens.Include(e => e.DienVien).Include(e => e.PhanPhim).Load();
            dbContext.CT_TheLoais.Include(e => e.PhanPhim).Include(e => e.TheLoai).Load();
            dbContext.CT_Hangs.Include(e => e.Hang).Include(e => e.PhanPhim).Load();
            return dbContext;
        }
        public static DbContextOptions<AppDbContext> getApplicationDbContext()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            var con = configuration.GetConnectionString("ConnectionString"); 
            var contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(con).Options;
            return contextOptions;
        }
    }
}
