using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.Data.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Public;

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

        builder.Property(x => x.EmpreendimentoStatusId)
            .HasColumnName("status_fk")
            .IsRequired();

        builder.HasOne(x => x.EmpreendimentoStatus)
            .WithMany(x => x.Empreendimentos)
            .HasForeignKey(x => x.EmpreendimentoStatusId);
    }
}