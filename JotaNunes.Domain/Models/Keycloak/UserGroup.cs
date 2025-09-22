using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Keycloak;

public class UserGroup : BaseEntity
{
    public required Guid GroupId { get; set; }
    public required Guid UserId { get; set; }
    public required string MembershipType { get; set; }

    public required KeycloakGroup KeycloakGroup { get; set; }
    public required User User { get; set; }
}