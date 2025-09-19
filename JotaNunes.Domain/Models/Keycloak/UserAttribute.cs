using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Keycloak;

public class UserAttribute : BaseEntity
{
    public new Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Value { get; set; }
    public Guid UserId { get; set; }
    
    public required User User { get; set; }
}