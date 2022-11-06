﻿// <auto-generated />
using FirstAttempt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FirstAttempt.Migrations
{
    [DbContext(typeof(FirstAttemptContext))]
    partial class FirstAttemptContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FirstAttempt.Models.Bikes", b =>
                {
                    b.Property<string>("BikeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BikeCount")
                        .HasColumnType("int");

                    b.Property<string>("BikeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BikePrice")
                        .HasColumnType("int");

                    b.Property<string>("BikeSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BikeId");

                    b.ToTable("Bikes");
                });
#pragma warning restore 612, 618
        }
    }
}
