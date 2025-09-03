using JotaNunes.Domain.Services;

namespace JotaNunes.Domain.Interfaces.Base;

public interface IBaseRepository<TEntity> 
    where TEntity : class
{
    IDomainService DomainService { get; }
    Task<TEntity?> GetByIdAsync(long id);
    Task InsertAsync(TEntity entity);
    void Update(TEntity entity);
}