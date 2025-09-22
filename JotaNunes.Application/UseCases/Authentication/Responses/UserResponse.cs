using JotaNunes.Domain.Extensions;
using JotaNunes.Domain.Models.Keycloak;
using System.Diagnostics.CodeAnalysis;

namespace JotaNunes.Application.UseCases.Authentication.Responses;

public class UserResponse
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    private List<Profile> _profiles = new();

    public required List<Profile> Profiles
    {
        get => _profiles.OrderBy(p => p.Id).ToList();
        set => _profiles = value;
    }
}

[method: SetsRequiredMembers]
public class Profile(UserGroup userGroup)
{
    public required int Id { get; set; } = (int)userGroup.GroupId.ToGroup();
    public required string Name { get; set; } = userGroup.KeycloakGroup.Name;
}