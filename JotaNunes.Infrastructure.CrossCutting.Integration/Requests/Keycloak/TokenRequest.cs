namespace JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

public class TokenRequest
{
    public required string GrantType { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
}