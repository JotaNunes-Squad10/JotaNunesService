using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Keycloak;

public class KeycloakGroup : BaseEntity
{
    public new Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid? ParentGroup { get; set; }
    public required string RealmId { get; set; }
    public required int Type { get; set; }
    public required string Description { get; set; }

    public required List<UserGroup> UserGroups { get; set; }
}