using JotaNunes.Domain.Models;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings;

public class EmpreendimentoMapping : BaseAuditEntityMapping<Empreendimento>
{
    public override void Configure(EntityTypeBuilder<Empreendimento> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("tb_empreendimento", "public");
        
        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .IsRequired();
        
        builder.Property(x => x.TamanhoArea)
            .HasColumnName("tamanho_area")
            .IsRequired();
        
        builder.Property(x => x.Localizacao)
            .HasColumnName("localizacao")
            .IsRequired();
    }
}