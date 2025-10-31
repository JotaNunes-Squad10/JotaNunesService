using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class EmpreendimentoBaseMapping : BaseAuditEntityMapping<EmpreendimentoBase>
{
    public override void Configure(EntityTypeBuilder<EmpreendimentoBase> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_empreendimento_base", "public");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status_fk")
            .IsRequired();

        builder.HasOne(x => x.EmpreendimentoStatus)
            .WithMany(x => x.EmpreendimentosBase)
            .HasForeignKey(x => x.Status);
    }
}