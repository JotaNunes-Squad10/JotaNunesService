using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Keycloak;

public class UserGroupMapping : BaseEntityMapping<UserGroup>
{
    public override void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.ToTable("user_group_membership", "keycloak");

        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.UserId, x.GroupId });

        builder.Property(x => x.GroupId)
            .HasColumnName("group_id")
            .HasColumnType("varchar")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("varchar")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.MembershipType)
            .HasColumnName("membership_type")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(x => x.KeycloakGroup)
            .WithMany(x => x.UserGroups)
            .HasForeignKey(x => x.GroupId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserGroups)
            .HasForeignKey(x => x.UserId);
    }
}