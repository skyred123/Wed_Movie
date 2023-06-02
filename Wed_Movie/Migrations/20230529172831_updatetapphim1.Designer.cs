﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wed_Movie.Data;

#nullable disable

namespace Wed_Movie.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230529172831_updatetapphim1")]
    partial class updatetapphim1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.2.23128.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Wed_Movie.Data.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Wed_Movie.Models.CT_DienVien", b =>
                {
                    b.Property<string>("IdDienVien")
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(1);

                    b.Property<string>("IdPhanPhim")
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(2);

                    b.HasKey("IdDienVien", "IdPhanPhim");

                    b.HasIndex("IdPhanPhim");

                    b.ToTable("CT_DienViens");
                });

            modelBuilder.Entity("Wed_Movie.Models.CT_Hang", b =>
                {
                    b.Property<string>("IdPhanPhim")
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(2);

                    b.Property<string>("IdHang")
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(1);

                    b.HasKey("IdPhanPhim", "IdHang");

                    b.HasIndex("IdHang");

                    b.ToTable("CT_Hangs");
                });

            modelBuilder.Entity("Wed_Movie.Models.CT_TheLoai", b =>
                {
                    b.Property<string>("IdPhanPhim")
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(2);

                    b.Property<string>("IdTheLoai")
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(1);

                    b.HasKey("IdPhanPhim", "IdTheLoai");

                    b.HasIndex("IdTheLoai");

                    b.ToTable("CT_TheLoais");
                });

            modelBuilder.Entity("Wed_Movie.Models.DienVien", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DienViens");
                });

            modelBuilder.Entity("Wed_Movie.Models.Hang", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hangs");
                });

            modelBuilder.Entity("Wed_Movie.Models.PhanPhim", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhimId")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PublicYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeUpdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trailer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PhimId");

                    b.ToTable("PhanPhims");
                });

            modelBuilder.Entity("Wed_Movie.Models.Phim", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Phims");
                });

            modelBuilder.Entity("Wed_Movie.Models.TapPhim", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhanPhimId")
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("TimeUpdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlVideo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PhanPhimId");

                    b.ToTable("TapPhims");
                });

            modelBuilder.Entity("Wed_Movie.Models.TheLoai", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TheLoais");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Wed_Movie.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Wed_Movie.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wed_Movie.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Wed_Movie.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Wed_Movie.Models.CT_DienVien", b =>
                {
                    b.HasOne("Wed_Movie.Models.DienVien", "DienVien")
                        .WithMany("CT_DienViens")
                        .HasForeignKey("IdDienVien")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wed_Movie.Models.PhanPhim", "PhanPhim")
                        .WithMany("CT_DienVien")
                        .HasForeignKey("IdPhanPhim")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DienVien");

                    b.Navigation("PhanPhim");
                });

            modelBuilder.Entity("Wed_Movie.Models.CT_Hang", b =>
                {
                    b.HasOne("Wed_Movie.Models.Hang", "Hang")
                        .WithMany("CT_Hang")
                        .HasForeignKey("IdHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wed_Movie.Models.PhanPhim", "PhanPhim")
                        .WithMany("CT_Hangs")
                        .HasForeignKey("IdPhanPhim")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hang");

                    b.Navigation("PhanPhim");
                });

            modelBuilder.Entity("Wed_Movie.Models.CT_TheLoai", b =>
                {
                    b.HasOne("Wed_Movie.Models.PhanPhim", "PhanPhim")
                        .WithMany("CT_TheLoais")
                        .HasForeignKey("IdPhanPhim")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wed_Movie.Models.TheLoai", "TheLoai")
                        .WithMany("CT_TheLoais")
                        .HasForeignKey("IdTheLoai")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhanPhim");

                    b.Navigation("TheLoai");
                });

            modelBuilder.Entity("Wed_Movie.Models.PhanPhim", b =>
                {
                    b.HasOne("Wed_Movie.Models.Phim", "Phim")
                        .WithMany("PhanPhim")
                        .HasForeignKey("PhimId");

                    b.Navigation("Phim");
                });

            modelBuilder.Entity("Wed_Movie.Models.TapPhim", b =>
                {
                    b.HasOne("Wed_Movie.Models.PhanPhim", "PhanPhim")
                        .WithMany("TapPhim")
                        .HasForeignKey("PhanPhimId");

                    b.Navigation("PhanPhim");
                });

            modelBuilder.Entity("Wed_Movie.Models.DienVien", b =>
                {
                    b.Navigation("CT_DienViens");
                });

            modelBuilder.Entity("Wed_Movie.Models.Hang", b =>
                {
                    b.Navigation("CT_Hang");
                });

            modelBuilder.Entity("Wed_Movie.Models.PhanPhim", b =>
                {
                    b.Navigation("CT_DienVien");

                    b.Navigation("CT_Hangs");

                    b.Navigation("CT_TheLoais");

                    b.Navigation("TapPhim");
                });

            modelBuilder.Entity("Wed_Movie.Models.Phim", b =>
                {
                    b.Navigation("PhanPhim");
                });

            modelBuilder.Entity("Wed_Movie.Models.TheLoai", b =>
                {
                    b.Navigation("CT_TheLoais");
                });
#pragma warning restore 612, 618
        }
    }
}
