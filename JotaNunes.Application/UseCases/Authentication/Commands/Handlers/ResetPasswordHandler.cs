using JotaNunes.Application.UseCases.Authentication.Commands.Requests;
using JotaNunes.Application.UseCases.Authentication.Responses;
using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using KeycloakResetPasswordRequest = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.ResetPasswordRequest;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Handlers;

public class ResetPasswordHandler(
    IDomainService domainService,
    IKeycloakService keycloakService,
    IUserRepository repository)
    : BaseHandler<User, ResetPasswordRequest, UserResponse, IUserRepository>(domainService, repository),
    IRequestHandler<ResetPasswordRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await Repository.GetByIdAsync(request.UserId);

            if (IsNull(user)) return Response();

            var resetPasswordRequest = new KeycloakResetPasswordRequest
            {
                Type = "password",
                Value = request.NewPassword,
                Temporary = true
            };

            var response = await keycloakService.ResetPassword(request.UserId, resetPasswordRequest);

            return Response(response);
        }
        catch (Exception)
        {
            return Response();
        }
    }
}