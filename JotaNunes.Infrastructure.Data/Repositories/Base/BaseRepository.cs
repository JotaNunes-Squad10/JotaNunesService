using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Base;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories.Base;

public abstract class BaseRepository<TEntity>(ApplicationContext applicationContext, IDomainService domainService)
    : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _applicationDbSet = applicationContext.Set<TEntity>();

    public IDomainService DomainService => domainService;

    protected IQueryable<TEntity> Get
        => _applicationDbSet.AsNoTracking();

    protected IQueryable<TEntity> GetTracking
        => _applicationDbSet.AsQueryable<TEntity>();

    protected IQueryable<T> GetFromSql<T>(string sql, params object[] parameters) where T : class
        => applicationContext.Set<T>().FromSqlRaw(sql, parameters);

    public virtual async Task<List<TEntity>> GetAllAsync()
        => await GetTracking.OrderBy(x => x.Id).ToListAsync();

    public virtual async Task<TEntity?> GetByIdAsync(long id)
        => await GetTracking.FirstOrDefaultAsync(x => x.Id == id);

    public async Task InsertAsync(TEntity entity)
        => await _applicationDbSet.AddAsync(entity);

    public virtual void Update(TEntity entity)
        => _applicationDbSet.Update(entity);
}