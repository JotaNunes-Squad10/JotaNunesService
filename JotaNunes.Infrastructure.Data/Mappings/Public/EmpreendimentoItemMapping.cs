using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

public class EmpreendimentoItemMapping : BaseAuditEntityMapping<EmpreendimentoItem>
{
    public override void Configure(EntityTypeBuilder<EmpreendimentoItem> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("rl_empreendimento_item", "public");
        
        builder.Property(x => x.EmpreendimentoId)
            .HasColumnName("empreendimento_fk")
            .IsRequired();
        
        builder.Property(x => x.ItemId)
            .HasColumnName("item_fk")
            .IsRequired();
        
        builder.HasOne(x => x.Empreendimento)
            .WithMany(x => x.EmpreendimentoItens)
            .HasForeignKey(x => x.EmpreendimentoId);
        
        builder.HasOne(x => x.Item)
            .WithMany(x => x.EmpreendimentoItens)
            .HasForeignKey(x => x.ItemId);
    }
}