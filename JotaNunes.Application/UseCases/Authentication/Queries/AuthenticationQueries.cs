using JotaNunes.Application.UseCases.Authentication.Responses;
using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Authentication.Queries;

public class AuthenticationQueries(IDomainService domainService, IUserRepository repository)
    : BaseQueries<User, UserResponse, IUserRepository>(domainService, repository), IAuthenticationQueries
{
    public new async Task<DefaultResponse> GetAllAsync()
    {
        var list = (await Repository.GetAllAsync())
            .Where(u => u.FirstName != null).ToList();

        if (ListIsNullOrEmpty(list)) return Response();

        var response = Map(list);

        return Response(response);
    }

    public virtual async Task<DefaultResponse> GetByIdAsync(Guid id)
    {
        var user = await Repository.GetByIdAsync(id);

        if (IsNull(user)) return Response();

        var response = Map(user!);

        return Response(response);
    }

    public virtual async Task<DefaultResponse> GetByUsernameAsync(string username)
    {
        var user = await Repository.GetByUsernameAsync(username);

        if (IsNull(user)) return Response();

        var response = Map(user!);

        return Response(response);
    }

    public virtual async Task<DefaultResponse> GetRequiredActionsByUsernameAsync(string username)
    {
        var user = await Repository.GetByUsernameAsync(username);

        var response = new RequiredActionsResponse
        {
            Username = username,
            RequiredActions = user?.UserRequiredActions.Select(ura => ura.Action).ToList() ?? []
        };

        return Response(response);
    }
}