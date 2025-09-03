using JotaNunes.Domain.Models;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings;

public class TopicoMapping : BaseAuditEntityMapping<Topico>
{
    public override void Configure(EntityTypeBuilder<Topico> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_topico", "public");

        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired();
    }
}