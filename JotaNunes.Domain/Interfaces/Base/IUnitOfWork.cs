namespace JotaNunes.Domain.Interfaces.Base;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(Guid userId);
}