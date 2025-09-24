namespace JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

public class UserRequest
{
    public required string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required bool Enabled { get; set; }
    public List<string>? Groups { get; set; }
    public Dictionary<string, List<string>>? Attributes { get; set; }
}

public class Attribute
{
    public required string Name { get; set; }
    public required string Value { get; set; }
}