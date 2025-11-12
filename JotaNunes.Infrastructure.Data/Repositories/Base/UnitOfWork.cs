using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Base;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;
using JotaNunes.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace JotaNunes.Infrastructure.Data.Repositories.Base;

public class UnitOfWork(ApplicationContext applicationContext, INotifications notifications)
    : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _currentTransaction;

    public async Task<bool> CommitAsync(Guid userId)
    {
        try
        {
            var entries = applicationContext.ChangeTracker.Entries<BaseAuditEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                    entry.Entity.AuditUpdate(userId);

                if (entry.State == EntityState.Added)
                    entry.Entity.AuditInsert(userId);
            }
            return await applicationContext.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            notifications.AddError("CommitAsync", $"{e.Message}: {e.InnerException?.Message}");
            return false;
        }
    }

    public async Task ExecuteInStrategyAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken = default)
    {
        var strategy = applicationContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await operation(cancellationToken);
        });
    }

    public async Task<T> ExecuteInStrategyAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken = default)
    {
        var strategy = applicationContext.Database.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            return await operation(cancellationToken);
        });
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
            return;

        _currentTransaction = await applicationContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
            return;

        try
        {
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
            return;

        try
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        applicationContext.Dispose();
    }
}