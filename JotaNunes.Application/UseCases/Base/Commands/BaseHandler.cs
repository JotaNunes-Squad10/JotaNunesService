using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Base;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.ErrorMessages;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;

namespace JotaNunes.Application.UseCases.Base.Commands;

public abstract class BaseHandler<TEntity, TRequest, TResponse, TRepository>(IDomainService domainService, TRepository repository)
    : BaseUseCase<TEntity, TResponse, TRepository>(domainService, repository)
    where TEntity : BaseAuditEntity
    where TRequest : class
    where TResponse : class
    where TRepository : IBaseRepository<TEntity>
{
    private readonly IDomainService _domainService = domainService;
    protected ExternalServices ExternalServices => _domainService.AppProvider.ExternalServices;
    
    private async Task<TResponse?> CommitAsync(TEntity entity)
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

    protected async Task<TResponse?> InsertAsync(TRequest request)
        => await InsertAsync(Map(request));

    protected async Task<TResponse?> InsertAsync(TEntity entity)
    {
        if (!IsNull(entity))
            await Repository.InsertAsync(entity);

        return await CommitAsync(entity);
    }

    protected async Task<TResponse?> UpdateAsync(TRequest request)
    {
        var entity = await Repository.GetByIdAsync(Map(request).Id);
        
        if (IsNull(entity)) return null;
        
        Map(request, entity);
        
        return await UpdateAsync(entity!);
    }

    protected async Task<TResponse?> UpdateAsync(TEntity entity)
    {
        Repository.Update(entity);
        return await CommitAsync(entity);
    }

    protected async Task<TResponse?> DeleteAsync(long id)
    {
        var entity = await Repository.GetByIdAsync(id);
        
        if (IsNull(entity)) return null;
        
        return await DeleteAsync(entity!);
    }

    protected async Task<TResponse?> DeleteAsync(TEntity entity)
    {
        entity.Delete();
        return await UpdateAsync(entity);
    }
}