using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class RevisaoTopicoMapping : BaseAuditEntityMapping<RevisaoTopico>
{
    public override void Configure(EntityTypeBuilder<RevisaoTopico> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_revisao_topico");

        builder.Property(x => x.TopicoId)
            .HasColumnName("topico_fk")
            .IsRequired();

        builder.Property(x => x.StatusId)
            .HasColumnName("status_fk")
            .IsRequired();

        builder.Property(x => x.Observacao)
            .HasColumnName("observacao")
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(x => x.EmpreendimentoTopico)
            .WithMany(x => x.RevisoesTopico)
            .HasForeignKey(x => x.TopicoId);

        builder.HasOne(x => x.StatusRevisao)
            .WithMany(x => x.RevisoesTopico)
            .HasForeignKey(x => x.StatusId);
    }
}