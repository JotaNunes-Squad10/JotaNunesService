using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;
using JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.Base;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Services;

public class KeycloakService : BaseIntegrationHttpService, IKeycloakService
{
    public KeycloakService(HttpClient httpClient, IDomainService domainService)
        : base(httpClient, domainService) { SetBaseAddress(ExternalServices.KeycloakService.Url); }

    private string _token = string.Empty;
    private DateTime _tokenExpiry;

    private async Task<string> GetMasterToken()
    {
        if (string.IsNullOrEmpty(_token) || DateTime.UtcNow >= _tokenExpiry)
        {
            var request = new AuthenticationRequest
            {
                ClientId = ExternalServices.KeycloakService.ClientId,
                ClientSecret = ExternalServices.KeycloakService.ClientSecret,
                GrantType = "client_credentials",
                Scope = "openid"
            };
            var response = await PostAsync<TokenResponse>($"{ExternalServices.KeycloakService.Token}", PrepareAuthenticationRequest(request));
            _token = response.AccessToken;
            _tokenExpiry = DateTime.UtcNow.AddSeconds(response.ExpiresIn - 30);
        }
        return _token;
    }

    public async Task<TokenResponse> Authenticate(AuthenticationRequest request)
        => await PostAsync<TokenResponse>($"{ExternalServices.KeycloakService.Token}", PrepareAuthenticationRequest(request));

    public async Task<UserResponse> CreateUser(CreateUserRequest request)
    {
        SetAuthorization("Bearer", await GetMasterToken());

        var response = await PostAsync($"{ExternalServices.KeycloakService.User}", PrepareCreateUserRequest(request));

        if (!response.IsSuccessStatusCode)
        {
            AddError(nameof(CreateUser), response);
            return new UserResponse
                { Message = "User creation failed." };
        }

        return new UserResponse
            { Message = "User created successfully." };
    }

    public async Task<UserResponse> UpdateUser(UpdateUserRequest request)
    {
        SetAuthorization("Bearer", await GetMasterToken());

        var response = await PutAsync($"{ExternalServices.KeycloakService.User}/{request.Id}", PrepareUpdateUserRequest(request));

        if (!response.IsSuccessStatusCode)
        {
            AddError(nameof(UpdateUser), response);
            return new UserResponse
                { Message = "User creation failed." };
        }

        return new UserResponse
            { Message = "User updated successfully." };
    }

    public async Task<UserResponse> AddUserGroup(UpdateUserGroupsRequest request)
    {
        SetAuthorization("Bearer", await GetMasterToken());

        var response = await PutAsync($"{ExternalServices.KeycloakService.User}/{request.UserId}/groups/{request.GroupId}", PrepareUpdateUserGroupsRequest(request));

        if (!response.IsSuccessStatusCode)
        {
            AddError(nameof(UpdateUser), response);
            return new UserResponse
                { Message = "User group addition failed." };
        }

        return new UserResponse
            { Message = "User group added successfully." };
    }

    public async Task<UserResponse> RemoveUserGroup(UpdateUserGroupsRequest request)
    {
        SetAuthorization("Bearer", await GetMasterToken());

        var response = await DeleteAsync($"{ExternalServices.KeycloakService.User}/{request.UserId}/groups/{request.GroupId}");

        if (!response.IsSuccessStatusCode)
        {
            AddError(nameof(UpdateUser), response);
            return new UserResponse
                { Message = "User group removal failed." };
        }

        return new UserResponse
            { Message = "User group removed successfully." };
    }
}