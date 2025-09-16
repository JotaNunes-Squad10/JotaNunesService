using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Base.Queries;

public abstract class BaseQueries<TEntity, TResponse, TRepository>(IDomainService domainService, TRepository repository)
    : BaseUseCase<TEntity, TResponse, TRepository>(domainService, repository), IBaseQueries
    where TEntity : class
    where TResponse : class
    where TRepository : IBaseRepository<TEntity>
{
    public async Task<DefaultResponse> GetAllAsync()
    {
        var list = await Repository.GetAllAsync();

        if (ListIsNullOrEmpty(list)) return Response();

        return Response(Map(list));
    }

    public virtual async Task<DefaultResponse> GetByIdAsync(long id)
    {
        var entity = await Repository.GetByIdAsync(id);

        if (IsNull(entity)) return Response();

        return Response(Map(entity!));
    }

    public void AddError(Exception e)
        => AddError(typeof(TEntity).Name, "Query error:", e);
}