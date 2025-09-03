using JotaNunes.Domain.Models;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings;

public class ItemMapping : BaseAuditEntityMapping<Item>
{
    public override void Configure(EntityTypeBuilder<Item> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_item", "public");
        
        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired();
    }
}