using JotaNunes.Application.UseCases.Authentication.Commands.Requests;
using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Domain.Extensions;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;
using KeycloakUpdateUserRequest = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.UpdateUserRequest;
using UpdateUserGroupsRequest = JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak.UpdateUserGroupsRequest;
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
                Username = request.Username ?? user!.Username,
                FirstName = request.FirstName ?? user!.FirstName,
                LastName = request.LastName ?? user!.LastName,
                Email = request.Email ?? user!.Email,
                Enabled = user!.Enabled,
                Attributes = new()
                {
                    ["phone"] = new() { request.Phone ?? user!.Attributes?.FirstOrDefault(a => a.Name == "phone")?.Value ?? "" },
                    ["deleted"] = new() { user.Attributes?.FirstOrDefault(a => a.Name == "deleted")?.Value ?? "false" }
                }
            };

            var userResponse = await keycloakService.UpdateUser(updateUserRequest);

            user.UserGroups.ForEach(async void (group) =>
            {
                var removeUserGroupRequest = new UpdateUserGroupsRequest
                {
                    UserId = request.Id,
                    GroupId = group.KeycloakGroup.Id
                };
                var removeUserGroupResponse = await keycloakService.RemoveUserGroup(removeUserGroupRequest);
            });

            var addUserGroupRequest = new UpdateUserGroupsRequest
            {
                UserId = request.Id,
                GroupId = ((Group)request.Profile).ToGuid()
            };

            var addUserGroupResponse = await keycloakService.AddUserGroup(addUserGroupRequest);

            return Response(userResponse);
        }
        catch (Exception)
        {
            return Response();
        }
    }
}