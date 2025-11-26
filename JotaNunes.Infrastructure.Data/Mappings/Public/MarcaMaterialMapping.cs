using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class MarcaMaterialMapping : BaseAuditEntityMapping<MarcaMaterial>
{
    public override void Configure(EntityTypeBuilder<MarcaMaterial> builder)
    {
        base.Configure(builder);

        builder.ToTable("rl_marca_material", "public");

        builder.Property(x => x.MarcaId)
            .HasColumnName("marca_fk")
            .IsRequired();

        builder.Property(x => x.MaterialId)
            .HasColumnName("material_fk")
            .IsRequired();

        builder.HasOne(x => x.Marca)
            .WithMany(x => x.MarcaMateriais)
            .HasForeignKey(x => x.MarcaId);

        builder.HasOne(x => x.Material)
            .WithMany(x => x.MarcaMateriais)
            .HasForeignKey(x => x.MaterialId);
    }
}