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

        builder.Property(x => x.Guid)
            .HasColumnName("guid")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Localizacao)
            .HasColumnName("localizacao")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.TamanhoArea)
            .HasColumnName("tamanho_area")
            .IsRequired();

        builder.Property(x => x.Padrao)
            .HasColumnName("padrao_fk")
            .IsRequired();

        builder.Property(x => x.Versao)
            .HasColumnName("versao")
            .IsRequired();

        builder.HasOne(x => x.EmpreendimentoBase)
            .WithMany(x => x.Empreendimentos)
            .HasForeignKey(x => x.Guid);

        builder.HasOne(x => x.EmpreendimentoPadrao)
            .WithMany(x => x.Empreendimentos)
            .HasForeignKey(x => x.Padrao);
    }
}