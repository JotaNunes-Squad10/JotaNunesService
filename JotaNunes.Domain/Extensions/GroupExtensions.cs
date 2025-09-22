using JotaNunes.Domain.Models.Keycloak;
using System.ComponentModel;
using System.Reflection;

namespace JotaNunes.Domain.Extensions;

public static class GroupExtensions
{
    private static readonly Dictionary<Guid, Group> GroupMappings = new()
    {
        { new Guid("21d968fd-ecbb-4c21-a731-46a0250a7148"), Group.Administrador },
        { new Guid("1a887483-303d-43e8-8b73-e423da12d93a"), Group.Gestor },
        { new Guid("5cf659e5-a27b-4324-8d5e-5eb92d7b46e3"), Group.Operador }
    };

    public static string GetName(this Group group)
    {
        var fieldInfo = group.GetType().GetField(group.ToString());
        var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
        return descriptionAttribute?.Description ?? group.ToString();
    }

    /// <summary>
    /// Convert a Guid to the corresponding enum Group
    /// </summary>
    /// <param name="groupId">The Guid of the group to convert</param>
    /// <returns>The enum Group corresponding to the Guid</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the Guid is not found in the GroupMappings dictionary</exception>
    public static Group ToGroup(this Guid groupId)
    {
        if (GroupMappings.TryGetValue(groupId, out var group))
            return group;

        throw new KeyNotFoundException($"No mapping was found for the GroupId: {groupId}");
    }
}