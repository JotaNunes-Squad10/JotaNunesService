namespace JotaNunes.Domain.Models.Base;

public abstract class BaseAuditEntity : BaseEntity
{
    public long UsuarioInclusaoId { get; set; }
    public long UsuarioAlteracaoId { get; set; }
    public DateTime DataHoraInclusao { get; set; }
    public DateTime DataHoraAlteracao { get; set; }
    public bool Excluido { get; set; }

    public void AuditInsert(long userId)
    {
        UsuarioInclusaoId = userId;
        UsuarioAlteracaoId = userId;
        DataHoraInclusao = DateTime.UtcNow;
        DataHoraAlteracao = DateTime.UtcNow;
    }

    public void AuditUpdate(long userId)
    {
        UsuarioAlteracaoId = userId;
        DataHoraAlteracao = DateTime.UtcNow;
    }
    
    public void AuditDelete(long userId)
    {
        UsuarioAlteracaoId = userId;
        DataHoraAlteracao = DateTime.UtcNow;
        Excluido = true;
    }
}