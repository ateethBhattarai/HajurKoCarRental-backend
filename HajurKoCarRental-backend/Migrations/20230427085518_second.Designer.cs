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
    [Migration("20230427085518_second")]
    partial class second
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

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("model_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("model_number")
                        .HasColumnType("int");

                    b.Property<byte[]>("photo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("rental_amount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.DamagedCarsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<string>("damaged_parts")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("settlement_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DamagedCars");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.RentalModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("carsId")
                        .HasColumnType("int");

                    b.Property<bool>("discount")
                        .HasColumnType("bit");

                    b.Property<int>("rental_amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("rental_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("rental_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("userid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("carsId");

                    b.HasIndex("userid");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.UserModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_of_birth")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("document")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("email_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("profile_picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HajurKoCarRental_backend.Model.RentalModel", b =>
                {
                    b.HasOne("HajurKoCarRental_backend.Model.CarsModel", "cars")
                        .WithMany()
                        .HasForeignKey("carsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HajurKoCarRental_backend.Model.UserModel", "user")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cars");

                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}
