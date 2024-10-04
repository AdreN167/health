﻿// <auto-generated />
using System;
using Health.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Health.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DietDish", b =>
                {
                    b.Property<long>("DietsId")
                        .HasColumnType("bigint");

                    b.Property<long>("DishesId")
                        .HasColumnType("bigint");

                    b.HasKey("DietsId", "DishesId");

                    b.HasIndex("DishesId");

                    b.ToTable("DietDish");
                });

            modelBuilder.Entity("DietProduct", b =>
                {
                    b.Property<long>("DietsId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductsId")
                        .HasColumnType("bigint");

                    b.HasKey("DietsId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("DietProduct");
                });

            modelBuilder.Entity("DishProduct", b =>
                {
                    b.Property<long>("DishesId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductsId")
                        .HasColumnType("bigint");

                    b.HasKey("DishesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("DishProduct");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Diet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("GoalId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("GoalId");

                    b.ToTable("Diets");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.DietEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("Calories")
                        .HasColumnType("double precision");

                    b.Property<double>("Carbohydrates")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("DietId")
                        .HasColumnType("bigint");

                    b.Property<double>("Fats")
                        .HasColumnType("double precision");

                    b.Property<double>("Proteins")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("DietId");

                    b.ToTable("DietEvents");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Dish", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Exercise", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("CaloriesBurned")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long?>("TrainerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Goal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("Calories")
                        .HasColumnType("double precision");

                    b.Property<double>("Carbohydrates")
                        .HasColumnType("double precision");

                    b.Property<double>("Fats")
                        .HasColumnType("double precision");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<double>("Proteins")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Trainer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.UserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Workout", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("GoalId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("GoalId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.WorkoutEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("BurnedCalories")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("WorkoutId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutEvents");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.WorkoutExercise", b =>
                {
                    b.Property<long>("WorkoutId")
                        .HasColumnType("bigint");

                    b.Property<long>("ExerciseId")
                        .HasColumnType("bigint");

                    b.Property<int>("Repetitions")
                        .HasColumnType("integer");

                    b.HasKey("WorkoutId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("WorkoutExercise", (string)null);
                });

            modelBuilder.Entity("DietDish", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.Diet", null)
                        .WithMany()
                        .HasForeignKey("DietsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Health.Domain.Models.Entities.Dish", null)
                        .WithMany()
                        .HasForeignKey("DishesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DietProduct", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.Diet", null)
                        .WithMany()
                        .HasForeignKey("DietsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Health.Domain.Models.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DishProduct", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.Dish", null)
                        .WithMany()
                        .HasForeignKey("DishesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Health.Domain.Models.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Diet", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.Goal", "Goal")
                        .WithMany("Diets")
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goal");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.DietEvent", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.Diet", "Diet")
                        .WithMany("EventJournal")
                        .HasForeignKey("DietId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diet");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Exercise", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.Trainer", "Trainer")
                        .WithMany("Exercises")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Goal", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.User", "User")
                        .WithMany("Goals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.UserToken", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.User", "User")
                        .WithOne("UserToken")
                        .HasForeignKey("Health.Domain.Models.Entities.UserToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Workout", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.Goal", "Goal")
                        .WithMany("Workouts")
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goal");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.WorkoutEvent", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.Workout", "Workout")
                        .WithMany("EventJournal")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.WorkoutExercise", b =>
                {
                    b.HasOne("Health.Domain.Models.Entities.Exercise", "Exercise")
                        .WithMany("WorkoutExercise")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Health.Domain.Models.Entities.Workout", "Workout")
                        .WithMany("WorkoutExercise")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Diet", b =>
                {
                    b.Navigation("EventJournal");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Exercise", b =>
                {
                    b.Navigation("WorkoutExercise");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Goal", b =>
                {
                    b.Navigation("Diets");

                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Trainer", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.User", b =>
                {
                    b.Navigation("Goals");

                    b.Navigation("UserToken");
                });

            modelBuilder.Entity("Health.Domain.Models.Entities.Workout", b =>
                {
                    b.Navigation("EventJournal");

                    b.Navigation("WorkoutExercise");
                });
#pragma warning restore 612, 618
        }
    }
}
