using JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;
using JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;

public interface IKeycloakService
{
    Task<TokenResponse> Authenticate(AuthenticationRequest request);
    Task<UserResponse> CreateUser(CreateUserRequest request);
    Task<UserResponse> UpdateUser(UpdateUserRequest request);
    // Task<UserResponse> DeleteUser(CreateUserRequest request);
}