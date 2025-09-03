using JotaNunes.Domain.Models;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings;

public class AmbienteMapping : BaseAuditEntityMapping<Ambiente>
{
    public override void Configure(EntityTypeBuilder<Ambiente> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_ambiente", "public");
        
        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired();
    }
}