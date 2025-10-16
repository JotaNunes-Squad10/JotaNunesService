using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class EmpreendimentoAmbienteMapping : BaseAuditEntityMapping<EmpreendimentoAmbiente>
{
    public override void Configure(EntityTypeBuilder<EmpreendimentoAmbiente> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("rl_empreendimento_ambiente", "public");
        
        builder.Property(x => x.EmpreendimentoId)
            .HasColumnName("empreendimento_fk")
            .IsRequired();
        
        builder.Property(x => x.AmbienteId)
            .HasColumnName("ambiente_fk")
            .IsRequired();
        
        builder.HasOne(x => x.Empreendimento)
            .WithMany(x => x.EmpreendimentoAmbientes)
            .HasForeignKey(x => x.EmpreendimentoId);
        
        builder.HasOne(x => x.Ambiente)
            .WithMany(x => x.EmpreendimentoAmbientes)
            .HasForeignKey(x => x.AmbienteId);
    }
}