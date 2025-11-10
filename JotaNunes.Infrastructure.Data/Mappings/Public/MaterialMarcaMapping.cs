using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class MaterialMarcaMapping : BaseAuditEntityMapping<MaterialMarca>
{
    public override void Configure(EntityTypeBuilder<MaterialMarca> builder)
    {
        base.Configure(builder);

        builder.ToTable("rl_material_marca", "public");

        builder.Property(x => x.MaterialId)
            .HasColumnName("material_fk")
            .IsRequired();

        builder.Property(x => x.MarcaId)
            .HasColumnName("marca_fk")
            .IsRequired();

        builder.HasOne(x => x.Material)
            .WithMany(x => x.MaterialMarcas)
            .HasForeignKey(x => x.MaterialId);

        builder.HasOne(x => x.Marca)
            .WithMany(x => x.MaterialMarcas)
            .HasForeignKey(x => x.MarcaId);
    }
}