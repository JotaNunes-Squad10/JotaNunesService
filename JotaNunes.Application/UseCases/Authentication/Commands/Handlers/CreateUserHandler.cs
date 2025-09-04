using JotaNunes.Application.UseCases.Base;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;
using CreateUserRequest = JotaNunes.Application.UseCases.Authentication.Commands.Requests.CreateUserRequest;
using KeycloakUser = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.CreateUserRequest;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Handlers;

public class CreateUserHandler(
    IDomainService domainService,
    IKeycloakService keycloakService,
    IUserRepository repository
) : BaseHandler<User, CreateUserRequest, CreateUserResponse, IBaseRepository<User>>(domainService, repository),
    IRequestHandler<CreateUserRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var keycloakRequest = new KeycloakUser
            {
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Enabled = true,
                Groups = [((Group)request.Profile).GetName()],
                Credentials = [new() { Type = "password", Value = request.Password }]
            };
            var response = await keycloakService.CreateUser(keycloakRequest);
            return Response(response);
        }
        catch (Exception)
        {
            return Response();
        }
    }
}