namespace JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;

public class UserAttributeResponse
{
    public required string Name { get; set; }
    public string? Value { get; set; }
}