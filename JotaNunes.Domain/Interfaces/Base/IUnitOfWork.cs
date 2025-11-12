using System.Threading;
namespace JotaNunes.Domain.Interfaces.Base;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(Guid userId);

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    Task ExecuteInStrategyAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken = default);
    Task<T> ExecuteInStrategyAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken = default);
}