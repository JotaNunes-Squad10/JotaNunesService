namespace JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

public class UserRequest
{
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool? Enabled { get; set; }
    public Dictionary<string, List<string>>? Attributes { get; set; }
}