﻿// <auto-generated />
using System;
using Bikes_and_Parts_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bikes_and_Parts_Management_System.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221029195828_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Bikes_and_Parts_Management_System.Models.BikesInventory", b =>
                {
                    b.Property<int>("BikeNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BikeCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BikeNumber");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("Bikes_and_Parts_Management_System.Models.PartsAndAccessoriesInventory", b =>
                {
                    b.Property<int>("ItemNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ItemNumber");

                    b.ToTable("PartsAndAccessories");
                });

            modelBuilder.Entity("Bikes_and_Parts_Management_System.Models.PurchaseRecord", b =>
                {
                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductPurchased")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PurchaseType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RentDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UID")
                        .HasColumnType("INTEGER");

                    b.ToTable("PurchaseRecords");
                });

            modelBuilder.Entity("Bikes_and_Parts_Management_System.Models.Roles", b =>
                {
                    b.Property<int>("UID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Bikes_and_Parts_Management_System.Models.Users", b =>
                {
                    b.Property<int>("UID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
