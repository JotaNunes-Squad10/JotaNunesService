namespace JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;

public class TokenResponse
{
    public required string AccessToken { get; set; }
    public required int ExpiresIn { get; set; }
}