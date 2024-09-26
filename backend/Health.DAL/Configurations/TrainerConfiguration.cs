using Health.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Health.DAL.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.FileName).HasMaxLength(200);
        builder.HasMany(t => t.Exercises)
            .WithOne(ex => ex.Trainer)
            .HasForeignKey(ex => ex.TrainerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}