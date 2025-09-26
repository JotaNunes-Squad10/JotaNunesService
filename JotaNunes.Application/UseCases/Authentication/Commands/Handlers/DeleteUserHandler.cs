using JotaNunes.Application.UseCases.Authentication.Commands.Requests;
using JotaNunes.Application.UseCases.Authentication.Responses;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using KeycloakUpdateUserRequest = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.UpdateUserRequest;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Handlers;

public class DeleteUserHandler(
    IDomainService domainService,
    IKeycloakService keycloakService,
    IUserRepository repository)
    : BaseHandler<User, DeleteUserRequest, UserResponse, IUserRepository>(domainService, repository),
    IRequestHandler<DeleteUserRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var updateUserRequest = new KeycloakUpdateUserRequest
        {
            Id = request.Id,
            Attributes = new() { ["deleted"] = new() { "true" } }
        };

        var response = await keycloakService.UpdateUser(updateUserRequest);

        return Response(response);
    }
}