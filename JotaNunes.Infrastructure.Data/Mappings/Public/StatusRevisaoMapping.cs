using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class StatusRevisaoMapping : BaseEntityMapping<StatusRevisao>
{
    public override void Configure(EntityTypeBuilder<StatusRevisao> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_status_revisao");

        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Excluido)
            .HasColumnName("fl_excluido")
            .IsRequired();
    }
}