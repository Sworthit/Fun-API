﻿// <auto-generated />
using System;
using FunApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FunApi.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20210721093035_DataSeedMigration")]
    partial class DataSeedMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("FunApi.Model.GeneratedName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("GeneratedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GeneratedName");
                });

            modelBuilder.Entity("FunApi.Model.Name", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Names");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            name = "Mat"
                        },
                        new
                        {
                            Id = 2,
                            name = "Maciek"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
