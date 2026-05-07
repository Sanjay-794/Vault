using Devnet.Vault.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Devnet.Vault.Infrastructure.Persistence.Configurations.Identity;

public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        builder.ToTable("UserDetails");

        // Primary Key
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .ValueGeneratedOnAdd();
        // Properties
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Email)
            .HasMaxLength(256);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(x => x.IsDeactivated)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.LastLoginDate);

        builder.Property(x => x.DeactivatedAt);

        // Indexes 
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();

        builder.Property(x => x.RoleId)
            .IsRequired();

        builder.Property(x => x.CountryId)
            .IsRequired();

        // Relationships
        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
