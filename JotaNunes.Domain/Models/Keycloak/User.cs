using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Keycloak;

public class User : BaseEntity, IUser
{
    public new Guid Id { get; set; }
    public string? Email { get; set; }
    public required bool EmailVerified { get; set; }
    public required bool Enabled { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Username { get; set; }
    
    public required List<UserAttribute> Attributes { get; set; }
}