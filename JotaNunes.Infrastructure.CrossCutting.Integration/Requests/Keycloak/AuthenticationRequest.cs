namespace JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

public class AuthenticationRequest
{
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string GrantType { get; set; }
    public required string Scope { get; set; } 
    public string? Username { get; set; }
    public string? Password { get; set; }
}