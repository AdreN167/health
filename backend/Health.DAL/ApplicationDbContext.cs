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

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}

