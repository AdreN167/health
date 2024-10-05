using Health.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Health.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Diet> Diets { get; set; }
    public DbSet<WorkoutEvent> WorkoutEvents{ get; set; }
    public DbSet<DietEvent> DietEvents{ get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // отключаем легаси формат времени
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder
            .Entity<Workout>()
            .HasMany(w => w.Exercises)
            .WithMany(e => e.Workouts)
            .UsingEntity<WorkoutExercise>(
                we => we
                    .HasOne(x => x.Exercise)
                    .WithMany(y => y.WorkoutExercise)
                    .HasForeignKey(x => x.ExerciseId),
                we => we
                    .HasOne(x => x.Workout)
                    .WithMany(y => y.WorkoutExercise)
                    .HasForeignKey(x => x.WorkoutId),
                we =>
                {
                    we.Property(x => x.Repetitions).IsRequired();
                    we.HasKey(x => new { x.WorkoutId, x.ExerciseId });
                    we.ToTable("WorkoutExercise");
                });

        modelBuilder
            .Entity<Product>()
            .HasMany(p => p.Dishes)
            .WithMany(d => d.Products)
            .UsingEntity<DishProduct>(
                dp => dp
                    .HasOne(x => x.Dish)
                    .WithMany(y => y.DishProducts)
                    .HasForeignKey(x => x.DishId),
                dp => dp
                    .HasOne(x => x.Product)
                    .WithMany(y => y.DishProducts)
                    .HasForeignKey(x => x.ProductId),
                dp =>
                {
                    dp.Property(x => x.Weight).IsRequired();
                    dp.HasKey(x => new { x.DishId, x.ProductId });
                    dp.ToTable("DishProduct");
                });

        base.OnModelCreating(modelBuilder);
    }
}

