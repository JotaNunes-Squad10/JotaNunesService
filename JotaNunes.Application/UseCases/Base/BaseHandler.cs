using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Base;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.ErrorMessages;

namespace JotaNunes.Application.UseCases.Base;

public abstract class BaseHandler<TEntity, TRequest, TResponse, TRepository>(IDomainService domainService, TRepository repository)
    : BaseUseCase<TEntity, TResponse, TRepository>(domainService, repository)
    where TEntity : BaseAuditEntity
    where TRequest : class
    where TResponse : class
    where TRepository : IBaseRepository<TEntity>
{
    private async Task<TResponse> CommitAsync(TEntity entity)
    {
        if (!IsNull(entity))
        {
            try
            {
                await CommitAsync();

                return Map(entity);
            }
            catch (Exception e)
            {
                AddError(typeof(TEntity).Name, ErrorMessage.ErrorPersistData, e);
            }
        }
        return null;
    }

    protected async Task CommitAsync()
    {
        try
        {
            await Repository.DomainService.UnitOfWork.CommitAsync(Repository.DomainService.User.Id);
        }
        catch (Exception e)
        {
            AddError(typeof(TRepository).Name, ErrorMessage.ErrorPersistData, e);
        }
    }
    
    public TEntity Map(TRequest request)
        => Map<TEntity, TRequest>(request);

    protected async Task<TResponse> InsertAsync(TRequest request)
        => await InsertAsync(Map(request));

    protected async Task<TResponse> InsertAsync(TEntity entity)
    {
        if (!IsNull(entity))
            await Repository.InsertAsync(entity);

        return await CommitAsync(entity);
    }
}