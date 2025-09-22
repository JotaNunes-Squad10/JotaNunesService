using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Keycloak;

public class UserMapping : BaseEntityMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user_entity", "keycloak");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("varchar")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(255);

        builder.Property(x => x.EmailVerified)
            .HasColumnName("email_verified")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.Enabled)
            .HasColumnName("enabled")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(255);

        builder.Property(x => x.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(255);

        builder.Property(x => x.Username)
            .HasColumnName("username")
            .HasMaxLength(255)
            .IsRequired();
    }
}