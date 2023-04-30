﻿// <auto-generated />
using System;
using HajurKoCarRental_backend.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HajurKoCarRental_backend.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20230430084913_login validation")]
    partial class loginvalidation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HajurKoCarRental_backend.Model.CarsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("availability_status")
                        .HasColumnType("int");

                    b.Property<string>("brand_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("model_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("registration_number")
                        .HasColumnType("int");

                    b.Property<double>("rental_cost")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.DamagedCarsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("carsId")
                        .HasColumnType("int");

                    b.Property<string>("damage_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("repair_cost")
                        .HasColumnType("int");

                    b.Property<string>("settlement_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("usersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("carsId");

                    b.HasIndex("usersId");

                    b.ToTable("DamagedCars");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.RentalHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("authorized_by")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("carsId")
                        .HasColumnType("int");

                    b.Property<int>("rental_charge")
                        .HasColumnType("int");

                    b.Property<int>("rental_duration")
                        .HasColumnType("int");

                    b.Property<DateTime>("requested_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("carsId");

                    b.HasIndex("userId");

                    b.ToTable("RentalHistory");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.RentalModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Cars_id")
                        .HasColumnType("int");

                    b.Property<int>("Users_id")
                        .HasColumnType("int");

                    b.Property<bool>("available_discount")
                        .HasColumnType("bit");

                    b.Property<double>("rental_amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("rental_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("rental_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Cars_id");

                    b.HasIndex("Users_id");

                    b.ToTable("RentalRequest");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_of_birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("last_login")
                        .HasColumnType("datetime2");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("profile_picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.DamagedCarsModel", b =>
                {
                    b.HasOne("HajurKoCarRental_backend.Model.CarsModel", "cars")
                        .WithMany()
                        .HasForeignKey("carsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HajurKoCarRental_backend.Model.UserModel", "users")
                        .WithMany()
                        .HasForeignKey("usersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cars");

                    b.Navigation("users");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.RentalHistory", b =>
                {
                    b.HasOne("HajurKoCarRental_backend.Model.CarsModel", "cars")
                        .WithMany()
                        .HasForeignKey("carsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HajurKoCarRental_backend.Model.UserModel", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cars");

                    b.Navigation("user");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.RentalModel", b =>
                {
                    b.HasOne("HajurKoCarRental_backend.Model.CarsModel", "Cars")
                        .WithMany()
                        .HasForeignKey("Cars_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HajurKoCarRental_backend.Model.UserModel", "Users")
                        .WithMany()
                        .HasForeignKey("Users_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cars");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
