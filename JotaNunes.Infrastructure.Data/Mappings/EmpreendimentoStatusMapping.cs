using JotaNunes.Domain.Models;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings;

public class EmpreendimentoStatusMapping : BaseEntityMapping<EmpreendimentoStatus>
{
    public override void Configure(EntityTypeBuilder<EmpreendimentoStatus> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_empreendimento_status", "public");
        
        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .IsRequired();
    }
}