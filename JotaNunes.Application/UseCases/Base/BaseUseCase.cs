using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.ErrorMessages;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Base;

public abstract class BaseUseCase<TEntity, TResponse, TRepository>(IDomainService domainService, TRepository repository)
    where TEntity : class
    where TResponse : class
    where TRepository : IBaseRepository<TEntity>
{
    private readonly IDomainService _domainService = domainService;

    protected TRepository Repository { get; } = repository;

    protected TDestination Map<TDestination, TSource>(TSource source)
        => _domainService.Mapper.Map<TDestination>(source);

    protected TDestination Map<TDestination>(object source, TDestination destination)
        => _domainService.Mapper.Map(source, destination);

    protected TDestination Map<TDestination>(object entity)
        => _domainService.Mapper.Map<TDestination>(entity);

    protected TResponse Map(TEntity entity)
        => Map<TResponse, TEntity>(entity);

    protected IEnumerable<TResponse> Map(IEnumerable<TEntity> listEntity)
        => Map<IEnumerable<TResponse>, IEnumerable<TEntity>>(listEntity);

    protected DefaultResponse Response(object? data = null)
    {
        if (HasError)
            _domainService.Notifications.LogErrors();

        _domainService.Response.Data = data;

        return _domainService.Response;
    }

    public void AddError(string property, string message)
        => _domainService.Notifications.AddError(property, message);

    public void AddError(string property, string message, Exception e)
        => AddError(property, $"{message} {e.Message} {e.InnerException?.Message}");

    public bool IsNull(object? value)
        => IsNull(value, ErrorMessage.ObjectNotFound);

    public bool IsNull(object? value, string message)
    {
        if (value == null)
        {
            AddError("IsNull", message);
            return true;
        }
        return false;
    }

    public bool ListIsNullOrEmpty(IEnumerable<TEntity> value)
        => ListIsNullOrEmpty<TEntity>(value);

    public bool ListIsNullOrEmpty<T>(IEnumerable<T>? value)
    {
        if (value == null || !value.Any())
        {
            AddError(typeof(IEnumerable<T>).Name, ErrorMessage.ListIsNullOrEmpty);
            return true;
        }
        return false;
    }

    private bool HasError => domainService.Notifications.HasError();
}