namespace JotaNunes.Domain.Models.Base;

public abstract class BaseAuditEntity : BaseEntity
{
    public Guid UsuarioInclusaoId { get; set; }
    public Guid UsuarioAlteracaoId { get; set; }
    public DateTime DataHoraInclusao { get; set; }
    public DateTime DataHoraAlteracao { get; set; }
    public bool Excluido { get; set; }

    public override void AuditInsert(Guid userId)
    {
        UsuarioInclusaoId = userId;
        UsuarioAlteracaoId = userId;
        DataHoraInclusao = DateTime.UtcNow;
        DataHoraAlteracao = DateTime.UtcNow;
    }

    public override void AuditUpdate(Guid userId)
    {
        UsuarioAlteracaoId = userId;
        DataHoraAlteracao = DateTime.UtcNow;
    }
    
    public override void Delete()
        => Excluido = true;
}