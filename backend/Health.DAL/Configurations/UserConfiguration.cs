using Health.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Health.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.Height).IsRequired();
        builder.Property(x => x.Weight).IsRequired();
        builder.HasMany(us => us.Goals)
            .WithOne(ex => ex.User)
            .HasForeignKey(ex => ex.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

