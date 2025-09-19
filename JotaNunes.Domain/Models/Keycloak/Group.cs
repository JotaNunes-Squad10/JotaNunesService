using System.ComponentModel;

namespace JotaNunes.Domain.Models.Keycloak;

public enum Group
{
    [Description("Administrador")]
    Administrador = 1,
    
    [Description("Gestor")]
    Gestor = 2,
    
    [Description("Operador")]
    Operador = 3
}