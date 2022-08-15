﻿// <auto-generated />
using System;
using MealPlanner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MealPlanner.Data.Migrations.MealPlannerDbContextSqLiteMigrations
{
    [DbContext(typeof(MealPlannerDbContextSqLite))]
    partial class MealPlannerDbContextSqLiteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("MealPlanEntityRecipeEntity", b =>
                {
                    b.Property<int>("MealPlansId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecipesID")
                        .HasColumnType("INTEGER");

                    b.HasKey("MealPlansId", "RecipesID");

                    b.HasIndex("RecipesID");

                    b.ToTable("MealPlanEntityRecipeEntity");
                });

            modelBuilder.Entity("MealPlanner.MealPlanEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MealPlans");
                });

            modelBuilder.Entity("MealPlanner.RecipeEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BookTitle")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Calories")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PageNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Time")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("MealPlanEntityRecipeEntity", b =>
                {
                    b.HasOne("MealPlanner.MealPlanEntity", null)
                        .WithMany()
                        .HasForeignKey("MealPlansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MealPlanner.RecipeEntity", null)
                        .WithMany()
                        .HasForeignKey("RecipesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
