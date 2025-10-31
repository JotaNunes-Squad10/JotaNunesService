using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class LogStatusMapping : BaseAuditEntityMapping<LogStatus>
{
    public override void Configure(EntityTypeBuilder<LogStatus> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_log_status", "public");

        builder.Property(x => x.EmpreendimentoId)
            .HasColumnName("empreendimento_fk")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status_fk")
            .IsRequired();

        builder.HasOne(x => x.Empreendimento)
            .WithMany(x => x.LogsStatus)
            .HasForeignKey(x => x.EmpreendimentoId);

        builder.HasOne(x => x.EmpreendimentoStatus)
            .WithMany(x => x.LogsStatus)
            .HasForeignKey(x => x.Status);
    }
}