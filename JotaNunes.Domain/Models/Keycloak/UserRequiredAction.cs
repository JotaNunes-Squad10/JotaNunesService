using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Keycloak;

public class UserRequiredAction : BaseEntity
{
    public required Guid UserId { get; set; }
    public required string Action { get; set; }
    public required User User { get; set; }
}