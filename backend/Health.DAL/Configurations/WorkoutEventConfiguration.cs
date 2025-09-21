using Health.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Health.DAL.Configurations;

public class WorkoutEventConfiguration : IEntityTypeConfiguration<WorkoutEvent>
{
    public void Configure(EntityTypeBuilder<WorkoutEvent> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.BurnedCalories).IsRequired();
        builder.HasOne(x => x.Workout)
            .WithMany(y => y.EventJournal)
            .HasForeignKey(x => x.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

