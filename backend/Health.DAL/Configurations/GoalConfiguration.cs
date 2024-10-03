using Health.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Health.DAL.Configurations;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(200);
        builder.Property(x => x.Deadline).IsRequired();
        builder.HasMany(x => x.Workouts)
            .WithOne(x => x.Goal)
            .HasForeignKey(x => x.GoalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

