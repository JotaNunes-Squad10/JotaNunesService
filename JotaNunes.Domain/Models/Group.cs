using System.ComponentModel;
using System.Reflection;

namespace JotaNunes.Domain.Models;

public static class GroupExtensions
{
    public static string GetName(this Group group)
    {
        var fieldInfo = group.GetType().GetField(group.ToString());
        var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
        return descriptionAttribute?.Description ?? group.ToString();
    }
}

public enum Group
{
    [Description("Administrador")]
    Administrador = 1,
    
    [Description("Gestor")]
    Gestor = 2,
    
    [Description("Operador")]
    Operador = 3
}