using FluentValidation;
using JotaNunes.Application.UseCases.Base.Commands.Requests;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.Behaviors;

public class PipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators, IDomainService domainService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : DefaultResponse
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            failures.ForEach(domainService.Notifications.AddError);

            if (domainService.Response is TResponse response)
                return response;
        }

        if (request is ITransactionalRequest)
        {
            return await domainService.UnitOfWork.ExecuteInStrategyAsync<TResponse>(async ct =>
            {
                await domainService.UnitOfWork.BeginTransactionAsync(ct);
                try
                {
                    var response = await next();
                    await domainService.UnitOfWork.CommitTransactionAsync(ct);
                    return response;
                }
                catch
                {
                    await domainService.UnitOfWork.RollbackTransactionAsync(ct);
                    if (domainService.Response is TResponse errorResponse)
                        return errorResponse;
                    throw;
                }
            }, cancellationToken);
        }

        return await next();
    }
}