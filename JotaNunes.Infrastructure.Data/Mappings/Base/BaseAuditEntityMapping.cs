using JotaNunes.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JotaNunes.Infrastructure.Data.Mappings.Base;

public class BaseAuditEntityMapping<TEntity>
    : BaseEntityMapping<TEntity>
    where TEntity : BaseAuditEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.UsuarioInclusaoId)
            .HasColumnName("usr_inc_fk")
            .IsRequired();

        builder.Property(x => x.UsuarioAlteracaoId)
            .HasColumnName("usr_alt_fk")
            .IsRequired();

        builder.Property(x => x.DataHoraInclusao)
            .HasColumnName("dt_hr_inc")
            .IsRequired();

        builder.Property(x => x.DataHoraAlteracao)
            .HasColumnName("dt_hr_alt")
            .IsRequired();

        builder.Property(x => x.Excluido)
            .HasColumnName("fl_excluido");

        builder.HasQueryFilter(x => !x.Excluido);
    }
}