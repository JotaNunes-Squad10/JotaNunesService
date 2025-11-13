using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class RevisaoAmbienteMapping : BaseAuditEntityMapping<RevisaoAmbiente>
{
    public override void Configure(EntityTypeBuilder<RevisaoAmbiente> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_revisao_ambiente");

        builder.Property(x => x.AmbienteId)
            .HasColumnName("ambiente_fk")
            .IsRequired();

        builder.Property(x => x.StatusId)
            .HasColumnName("status_fk")
            .IsRequired();

        builder.Property(x => x.Observacao)
            .HasColumnName("observacao")
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(x => x.TopicoAmbiente)
            .WithMany(x => x.RevisoesAmbiente)
            .HasForeignKey(x => x.AmbienteId);

        builder.HasOne(x => x.StatusRevisao)
            .WithMany(x => x.RevisoesAmbiente)
            .HasForeignKey(x => x.StatusId);
    }
}