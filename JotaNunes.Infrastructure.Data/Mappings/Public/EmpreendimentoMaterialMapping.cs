using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class EmpreendimentoMaterialMapping : BaseAuditEntityMapping<EmpreendimentoMaterial>
{
    public override void Configure(EntityTypeBuilder<EmpreendimentoMaterial> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("rl_empreendimento_material", "public");
        
        builder.Property(x => x.EmpreendimentoId)
            .HasColumnName("empreendimento_fk")
            .IsRequired();
        
        builder.Property(x => x.MaterialId)
            .HasColumnName("material_fk")
            .IsRequired();
        
        builder.HasOne(x => x.Empreendimento)
            .WithMany(x => x.EmpreendimentoMaterials)
            .HasForeignKey(x => x.EmpreendimentoId);
        
        builder.HasOne(x => x.Material)
            .WithMany(x => x.EmpreendimentoMateriais)
            .HasForeignKey(x => x.MaterialId);
    }
}