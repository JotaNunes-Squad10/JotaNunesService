using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Base;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;
using JotaNunes.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories.Base;

public class UnitOfWork(ApplicationContext applicationContext, INotifications notifications)
    : IUnitOfWork, IDisposable
{
    public async Task<bool> CommitAsync(long userId)
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