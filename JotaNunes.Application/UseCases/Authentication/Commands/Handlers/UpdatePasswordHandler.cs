using JotaNunes.Application.UseCases.Authentication.Commands.Requests;
using JotaNunes.Application.UseCases.Authentication.Responses;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using KeycloakAuthenticationRequest = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.AuthenticationRequest;
using KeycloakResetPasswordRequest = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.ResetPasswordRequest;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Handlers;

public class UpdatePasswordHandler(
    IDomainService domainService,
    IKeycloakService keycloakService,
    IUserRepository repository)
    : BaseHandler<User, UpdatePasswordRequest, UserResponse, IUserRepository>(domainService, repository),
    IRequestHandler<UpdatePasswordRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await Repository.GetByUsernameAsync(request.Username);

            if (IsNull(user)) return Response();

            var authenticationRequest = new KeycloakAuthenticationRequest
            {
                ClientId = ExternalServices.KeycloakService.ClientId,
                ClientSecret = ExternalServices.KeycloakService.ClientSecret,
                Username = request.Username,
                Password = request.CurrentPassword,
                GrantType = "password",
                Scope = "openid",
            };

            var authenticationResponse = await keycloakService.Authenticate(authenticationRequest);

            if (string.IsNullOrEmpty(authenticationResponse.AccessToken))
            {
                var errors = domainService.Notifications.GetErrors();
                var hasRequiredAction = false;

                errors.ForEach(e => {
                    if (e.Contains("Account is not fully set up"))
                    {
                        hasRequiredAction = true;
                        domainService.Notifications.ClearErrors();
                    }
                });

                if (!hasRequiredAction) return Response();
            }

            var resetPasswordRequest = new KeycloakResetPasswordRequest
            {
                Type = "password",
                Value = request.NewPassword,
                Temporary = false
            };

            var response = await keycloakService.ResetPassword(user!.Id, resetPasswordRequest);

            return Response(response);
        }
        catch (Exception)
        {
            return Response();
        }
    }
}