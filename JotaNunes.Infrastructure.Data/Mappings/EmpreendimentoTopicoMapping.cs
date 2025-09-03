using JotaNunes.Domain.Models;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings;

public class EmpreendimentoTopicoMapping : BaseAuditEntityMapping<EmpreendimentoTopico>
{
    public override void Configure(EntityTypeBuilder<EmpreendimentoTopico> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_empreendimento_topico", "public");
        
        builder.Property(x => x.EmpreendimentoId)
            .HasColumnName("empreendimento_fk")
            .IsRequired();
        
        builder.Property(x => x.TopicoId)
            .HasColumnName("topico_fk")
            .IsRequired();
        
        builder.HasOne(x => x.Empreendimento)
            .WithMany(x => x.EmpreendimentoTopicos)
            .HasForeignKey(x => x.EmpreendimentoId);
        
        builder.HasOne(x => x.Topico)
            .WithMany(x => x.EmpreendimentoTopicos)
            .HasForeignKey(x => x.TopicoId);
    }
}