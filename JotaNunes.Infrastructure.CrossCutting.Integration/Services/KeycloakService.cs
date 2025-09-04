using System.Text.Json;
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

    private async Task<string> GetToken()
    {
        if (string.IsNullOrEmpty(_token) || DateTime.UtcNow >= _tokenExpiry)
        {
            var request = new TokenRequest
            {
                ClientId = ExternalServices.KeycloakService.ClientId,
                ClientSecret = ExternalServices.KeycloakService.ClientSecret,
                GrantType = "client_credentials"
            };
            var response = await PostAsync<TokenResponse>($"{ExternalServices.KeycloakService.Token}", PrepareTokenRequest(request));
            _token = response.AccessToken;
            _tokenExpiry = DateTime.UtcNow.AddSeconds(response.ExpiresIn - 30);
        }
        return _token;
    }
    
    public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
    {
        SetAuthorization("Bearer", await GetToken());
        
        var response = await PostAsync($"{ExternalServices.KeycloakService.User}", PrepareCreateUserRequest(request));

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var error = JsonSerializer.Deserialize<ErrorResponse>(content);
            
            AddError(nameof(CreateUser), $"{response.StatusCode}: {error?.Error}");
            
            return new CreateUserResponse
                { Message = "User creation failed." };
        }
        
        return new CreateUserResponse
            { Message = "User created successfully." };
    }
}