using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class RevisaoMaterialMapping : BaseAuditEntityMapping<RevisaoMaterial>
{
    public override void Configure(EntityTypeBuilder<RevisaoMaterial> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_revisao_material");

        builder.Property(x => x.MaterialId)
            .HasColumnName("material_fk")
            .IsRequired();

        builder.Property(x => x.StatusId)
            .HasColumnName("status_fk")
            .IsRequired();

        builder.Property(x => x.Observacao)
            .HasColumnName("observacao")
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(x => x.TopicoMaterial)
            .WithMany(x => x.RevisoesMaterial)
            .HasForeignKey(x => x.MaterialId);

        builder.HasOne(x => x.StatusRevisao)
            .WithMany(x => x.RevisoesMaterial)
            .HasForeignKey(x => x.StatusId);
    }
}