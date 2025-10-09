using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class PadraoMapping : BaseAuditEntityMapping<Padrao>
{
    public override void Configure(EntityTypeBuilder<Padrao> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_padrao", "public");

        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .HasMaxLength(50)
            .IsRequired();
    }
}