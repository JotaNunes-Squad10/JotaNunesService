using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Keycloak;

public class UserRequiredActionMapping : BaseEntityMapping<UserRequiredAction>
{
    public override void Configure(EntityTypeBuilder<UserRequiredAction> builder)
    {
        builder.ToTable("user_required_action", "keycloak");

        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.UserId, x.Action });

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("varchar")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Action)
            .HasColumnName("required_action")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserRequiredActions)
            .HasForeignKey(x => x.UserId);
    }
}