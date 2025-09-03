namespace JotaNunes.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(long userId);
}