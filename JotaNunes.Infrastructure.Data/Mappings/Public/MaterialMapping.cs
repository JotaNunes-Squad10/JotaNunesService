using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class MaterialMapping : BaseAuditEntityMapping<Material>
{
    public override void Configure(EntityTypeBuilder<Material> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_material", "public");
        
        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired();

        builder.Property(x => x.MarcaId)
            .HasColumnName("marca_fk")
            .IsRequired();

        builder.HasOne(x => x.Marca)
            .WithMany(x => x.Materiais)
            .HasForeignKey(x => x.MarcaId);
    }
}