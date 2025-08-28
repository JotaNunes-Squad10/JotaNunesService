using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Base;

public abstract class BaseUseCase(IDomainService domainService)
{
    protected DefaultResponse Response(object data)
    {
        if (HasError)
            domainService.Notifications.LogErrors();

        domainService.Response.Data = data;

        return domainService.Response;
    }

    private bool HasError => domainService.Notifications.HasError();
}