using JotaNunes.Application.UseCases.Authentication.Commands.Requests;
using JotaNunes.Application.UseCases.Base;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;
using KeycloakAuthenticationRequest = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.AuthenticationRequest;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Handlers;

public class AuthenticationHandler(
    IDomainService domainService,
    IKeycloakService keycloakService,
    IUserRepository repository
) : BaseHandler<User, AuthenticationRequest, TokenResponse, IBaseRepository<User>>(domainService, repository),
    IRequestHandler<AuthenticationRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var authenticationRequest = new KeycloakAuthenticationRequest
            {
                ClientId = ExternalServices.KeycloakService.ClientId,
                ClientSecret = ExternalServices.KeycloakService.ClientSecret,
                Username = request.Username,
                Password = request.Password,
                GrantType = "password",
                Scope = "openid",
            };
            var response = await keycloakService.Authenticate(authenticationRequest);
            return Response(response);
        }
        catch (Exception)
        {
            return Response();
        }
    }
}