namespace JotaNunes.Domain.Models.Base;

public abstract class BaseEntity
{
    public long Id { get; set; }
    
    public virtual void AuditInsert(Guid userId) { }
    public virtual void AuditUpdate(Guid userId) { }
    public virtual void Delete() { }
}