using JotaNunes.Application.UseCases.Authentication.Responses;
using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Authentication.Queries;

public class AuthenticationQueries(IDomainService domainService, IUserRepository repository)
    : BaseQueries<Domain.Models.Keycloak.User, UserResponse, IUserRepository>(domainService, repository), IAuthenticationQueries
{
    public new async Task<DefaultResponse> GetAllAsync()
    {
        var list = (await Repository.GetAllAsync())
            .Where(u => u.FirstName != null).ToList();

        if (ListIsNullOrEmpty(list)) return Response();

        var response = new List<UserResponse>();

        foreach (var user in list)
        {
            response.Add(new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Attributes?.FirstOrDefault(a => a.Name == "phone")?.Value,
                RequiredActions = user.UserRequiredActions.Select(ura => ura.Action).ToList(),
                Profiles = user.UserGroups.Select(ug => new Profile(ug)).ToList()
            });
        }

        return Response(response);
    }

    public virtual async Task<DefaultResponse> GetByIdAsync(Guid id)
    {
        var entity = await Repository.GetByIdAsync(id);

        if (IsNull(entity)) return Response();

        var response = new UserResponse
        {
            Id = entity!.Id,
            Username = entity.Username,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.Attributes?.FirstOrDefault(a => a.Name == "phone")?.Value,
            RequiredActions = entity.UserRequiredActions.Select(ura => ura.Action).ToList(),
            Profiles = entity.UserGroups.Select(ug => new Profile(ug)).ToList()
        };

        return Response(response);
    }
}