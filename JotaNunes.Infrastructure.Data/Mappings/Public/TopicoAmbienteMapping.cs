using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

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

        builder.Property(x => x.Area)
            .HasColumnName("area");

        builder.Property(x => x.Posicao)
            .HasColumnName("posicao")
            .IsRequired();

        builder.Property(x => x.Versoes)
            .HasColumnName("versoes")
            .HasColumnType("int[]");

        builder.HasOne(x => x.EmpreendimentoTopico)
            .WithMany(x => x.TopicoAmbientes)
            .HasForeignKey(x => x.TopicoId);

        builder.HasOne(x => x.Ambiente)
            .WithMany(x => x.TopicoAmbientes)
            .HasForeignKey(x => x.AmbienteId);
    }
}