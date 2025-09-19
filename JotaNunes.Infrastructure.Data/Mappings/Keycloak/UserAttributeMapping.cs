using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Keycloak;

public class UserAttributeMapping : BaseEntityMapping<UserAttribute>
{
    public override void Configure(EntityTypeBuilder<UserAttribute> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("user_attribute", "keycloak");
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("varchar")
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasColumnName("value")
            .HasMaxLength(255);
        
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("varchar")
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Attributes)
            .HasForeignKey(x => x.UserId);
    }
}