namespace JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

public class UpdateUserRequest : UserRequest
{
    public required Guid Id { get; set; }
}