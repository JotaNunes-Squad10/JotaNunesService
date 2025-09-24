namespace JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

public class CreateUserRequest : UserRequest
{
    public required List<Credentials> Credentials { get; set; }
}

public class Credentials
{
    public required string Type { get; set; }
    public required string Value { get; set; }
    public bool Temporary { get; set; }
}