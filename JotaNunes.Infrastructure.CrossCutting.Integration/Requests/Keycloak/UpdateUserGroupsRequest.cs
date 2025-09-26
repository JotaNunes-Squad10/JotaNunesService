namespace JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

public class UpdateUserGroupsRequest
{
    public required Guid UserId { get; set; }
    public required Guid GroupId { get; set; }
}