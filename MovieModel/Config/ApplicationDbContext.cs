using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Wed_Movie.Entities;

namespace MovieModel.Config
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /*builder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            builder.Entity<IdentityUserRole<string>>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<IdentityUserToken<string>>()
                .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });*/
            /*base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);*/
            builder.Entity<CT_TheLoai>().HasKey(e => new { e.IdPhanPhim, e.IdTheLoai });
            builder.Entity<CT_Hang>().HasKey(e => new { e.IdPhanPhim, e.IdHang });
            builder.Entity<CT_DienVien>().HasKey(e => new { e.IdDienVien, e.IdPhanPhim });
        }
    }
}
