namespace JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

public class CreateUserRequest : UserRequest
{
    public List<string?>? Groups { get; set; }
    public required List<Credential> Credentials { get; set; }
}