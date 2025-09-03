using JotaNunes.Domain.Models;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings;

public class ItemMaterialMapping : BaseAuditEntityMapping<ItemMaterial>
{
    public override void Configure(EntityTypeBuilder<ItemMaterial> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_item_material", "public");
        
        builder.Property(x => x.ItemId)
            .HasColumnName("item_fk")
            .IsRequired();
        
        builder.Property(x => x.MaterialId)
            .HasColumnName("material_fk")
            .IsRequired();
        
        builder.HasOne(x => x.Item)
            .WithMany(x => x.ItemMateriais)
            .HasForeignKey(x => x.ItemId);
        
        builder.HasOne(x => x.Material)
            .WithMany(x => x.ItemMateriais)
            .HasForeignKey(x => x.MaterialId);
        
        builder.HasOne(x => x.Marca)
            .WithMany(x => x.ItemMateriais)
            .HasForeignKey(x => x.MarcaId);
    }
}