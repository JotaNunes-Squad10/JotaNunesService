using JotaNunes.Domain.Models.Keycloak;
using System.ComponentModel;
using System.Reflection;

namespace JotaNunes.Domain.Extensions;

public static class GroupExtensions
{
    public static string GetName(this Group group)
    {
        var fieldInfo = group.GetType().GetField(group.ToString());
        var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
        return descriptionAttribute?.Description ?? group.ToString();
    }
}