using JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;
using JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;

public interface IKeycloakService
{
    Task<CreateUserResponse> CreateUser(CreateUserRequest request);
}