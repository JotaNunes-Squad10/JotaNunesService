using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models;

public class User : BaseAuditEntity, IUser
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required Group Group { get; set; }
}