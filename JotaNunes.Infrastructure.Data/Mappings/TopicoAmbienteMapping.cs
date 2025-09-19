using JotaNunes.Domain.Models;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings;

public class TopicoAmbienteMapping : BaseAuditEntityMapping<TopicoAmbiente>
{
    public override void Configure(EntityTypeBuilder<TopicoAmbiente> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("rl_topico_ambiente", "public");
        
        builder.Property(x => x.TopicoId)
            .HasColumnName("topico_fk")
            .IsRequired();
        
        builder.Property(x => x.AmbienteId)
            .HasColumnName("ambiente_fk")
            .IsRequired();
        
        builder.HasOne(x => x.Topico)
            .WithMany(x => x.TopicoAmbientes)
            .HasForeignKey(x => x.TopicoId);
        
        builder.HasOne(x => x.Ambiente)
            .WithMany(x => x.TopicoAmbientes)
            .HasForeignKey(x => x.AmbienteId);
    }
}