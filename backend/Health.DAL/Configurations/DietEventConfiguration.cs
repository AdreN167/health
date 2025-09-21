using Health.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Health.DAL.Configurations;

public class DietEventConfiguration : IEntityTypeConfiguration<DietEvent>
{
    public void Configure(EntityTypeBuilder<DietEvent> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Proteins).IsRequired();
        builder.Property(x => x.Calories).IsRequired();
        builder.Property(x => x.Carbohydrates).IsRequired();
        builder.Property(x => x.Fats).IsRequired();
        builder.HasOne(x => x.Diet)
            .WithMany(y => y.EventJournal)
            .HasForeignKey(x => x.DietId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

