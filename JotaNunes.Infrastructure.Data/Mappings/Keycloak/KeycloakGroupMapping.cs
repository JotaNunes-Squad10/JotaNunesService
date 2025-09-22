using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JotaNunes.Infrastructure.Data.Mappings.Keycloak;

public class KeycloakGroupMapping : BaseEntityMapping<KeycloakGroup>
{
    public override void Configure(EntityTypeBuilder<KeycloakGroup> builder)
    {
        builder.ToTable("keycloak_group", "keycloak");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("varchar")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name");

        builder.Property(x => x.ParentGroup)
            .HasColumnName("parent_group")
            .HasColumnType("varchar")
            .HasMaxLength(36)
            .HasConversion(new ValueConverter<Guid?, string>(
                v => (v.HasValue ? v.Value.ToString() : null) ?? string.Empty,
                v => string.IsNullOrWhiteSpace(v) ? null : Guid.Parse(v)
            ));

        builder.Property(x => x.RealmId)
            .HasColumnName("realm_id")
            .HasColumnType("varchar")
            .HasMaxLength(36);

        builder.Property(x => x.Type)
            .HasColumnName("type")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(255);
    }
}