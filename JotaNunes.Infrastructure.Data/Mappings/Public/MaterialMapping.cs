using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class MaterialMapping : BaseAuditEntityMapping<Material>
{
    public override void Configure(EntityTypeBuilder<Material> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_material", "public");

        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired();
    }
}