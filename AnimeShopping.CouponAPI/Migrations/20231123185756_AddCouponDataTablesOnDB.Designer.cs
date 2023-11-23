﻿// <auto-generated />
using AnimeShopping.CouponAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnimeShopping.CouponAPI.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20231123185756_AddCouponDataTablesOnDB")]
    partial class AddCouponDataTablesOnDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AnimeShopping.CouponAPI.Model.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("coupon_code");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("discount_amount");

                    b.HasKey("Id");

                    b.ToTable("coupon");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CouponCode = "DSC_2022_A",
                            DiscountAmount = 10m
                        },
                        new
                        {
                            Id = 2L,
                            CouponCode = "DSC_2022_B",
                            DiscountAmount = 15m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
