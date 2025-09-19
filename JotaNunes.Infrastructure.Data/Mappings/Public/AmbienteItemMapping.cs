using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class AmbienteItemMapping : BaseAuditEntityMapping<AmbienteItem>
{
    public override void Configure(EntityTypeBuilder<AmbienteItem> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("rl_ambiente_item", "public");
        
        builder.Property(x => x.AmbienteId)
            .HasColumnName("ambiente_fk")
            .IsRequired();
        
        builder.Property(x => x.ItemId)
            .HasColumnName("item_fk")
            .IsRequired();
        
        builder.HasOne(x => x.Ambiente)
            .WithMany(x => x.AmbienteItems)
            .HasForeignKey(x => x.AmbienteId);
        
        builder.HasOne(x => x.Item)
            .WithMany(x => x.AmbienteItems)
            .HasForeignKey(x => x.ItemId);
    }
}