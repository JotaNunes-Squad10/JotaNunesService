using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class RevisaoItemMapping : BaseAuditEntityMapping<RevisaoItem>
{
    public override void Configure(EntityTypeBuilder<RevisaoItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_revisao_item");

        builder.Property(x => x.ItemId)
            .HasColumnName("item_fk")
            .IsRequired();

        builder.Property(x => x.StatusId)
            .HasColumnName("status_fk")
            .IsRequired();

        builder.Property(x => x.Observacao)
            .HasColumnName("observacao")
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(x => x.AmbienteItem)
            .WithMany(x => x.RevisoesItem)
            .HasForeignKey(x => x.ItemId);

        builder.HasOne(x => x.StatusRevisao)
            .WithMany(x => x.RevisoesItem)
            .HasForeignKey(x => x.StatusId);
    }
}