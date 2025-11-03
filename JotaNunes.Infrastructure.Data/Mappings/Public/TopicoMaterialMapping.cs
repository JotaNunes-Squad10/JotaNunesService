using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class TopicoMaterialMapping : BaseAuditEntityMapping<TopicoMaterial>
{
    public override void Configure(EntityTypeBuilder<TopicoMaterial> builder)
    {
        base.Configure(builder);

        builder.ToTable("rl_topico_material", "public");

        builder.Property(x => x.TopicoId)
            .HasColumnName("topico_fk")
            .IsRequired();

        builder.Property(x => x.MaterialId)
            .HasColumnName("material_fk")
            .IsRequired();

        builder.Property(x => x.Versoes)
            .HasColumnName("versoes")
            .HasColumnType("int[]");

        builder.HasOne(x => x.EmpreendimentoTopico)
            .WithMany(x => x.TopicoMateriais)
            .HasForeignKey(x => x.TopicoId);

        builder.HasOne(x => x.Material)
            .WithMany(x => x.EmpreendimentoMateriais)
            .HasForeignKey(x => x.MaterialId);
    }
}