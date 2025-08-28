using JotaNunes.Application.UseCases.Authentication.Commands.Requests;
using JotaNunes.Application.UseCases.Base;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using KeycloakCredentials = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.Credentials;
using KeycloakUser = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.CreateUserRequest;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Handlers;

public class CreateUserHandler(
    IDomainService domainService,
    IKeycloakService keycloakService
) : BaseUseCase(domainService),
    IRequestHandler<CreateUserRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var keycloakRequest = new KeycloakUser
            {
                Username = request.Username,
                Email = request.Email,
                Enabled = true,
                Credentials = new List<KeycloakCredentials>
                    { new() { Type = "password", Value = request.Password } }
            };
            var response = await keycloakService.CreateUser(keycloakRequest);
            return Response(response);
        }
        catch (Exception ex)
        {
            return Response(ex);
        }
    }
}