using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class MarcaMapping : BaseAuditEntityMapping<Marca>
{
    public override void Configure(EntityTypeBuilder<Marca> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_marca", "public");
        
        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired();
    }
}