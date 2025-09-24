using JotaNunes.Application.UseCases.Authentication.Commands.Requests;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Domain.Extensions;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;
using KeycloakUpdateUserRequest = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.UpdateUserRequest;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Handlers;

public class UpdateUserHandler(
    IDomainService domainService,
    IKeycloakService keycloakService,
    IUserRepository repository)
    : BaseHandler<User, UpdateUserRequest, UserResponse, IUserRepository>(domainService, repository),
    IRequestHandler<UpdateUserRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await Repository.GetByIdAsync(request.Id);

            if (IsNull(user)) return Response();

            var updateUserRequest = new KeycloakUpdateUserRequest
            {
                Id = request.Id,
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Enabled = user!.Enabled,
                Groups = [((Group)request.Profile).GetName()],
                Attributes = new()
                {
                    ["phone"] = new() { request.Phone },
                    ["deleted"] = new() { user.Attributes?.FirstOrDefault(a => a.Name == "deleted")?.Value ?? "false" }
                }
            };
            var response = await keycloakService.UpdateUser(updateUserRequest);
            return Response(response);
        }
        catch (Exception)
        {
            return Response();
        }
    }
}