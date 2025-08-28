namespace JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

public class CreateUserRequest
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required bool Enabled { get; set; }
    public required List<Credentials> Credentials { get; set; }
}

public class Credentials
{
    public required string Type { get; set; }
    public required string Value { get; set; }
    public bool Temporary { get; set; }
}