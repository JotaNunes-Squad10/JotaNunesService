namespace JotaNunes.Infrastructure.CrossCutting.Commons.Providers;

public class ApplicationProvider
{
    public required ExternalServices ExternalServices { get; set; }
    public string DataBase { get; set; } = string.Empty;
    public string DataBaseMask => ConnectionStringMask(DataBase, ";", new List<string> { "UserId", "Password" });

    private static string ConnectionStringMask(string value, string split, List<string> keys)
    {
        string result = string.Empty;

        var parts = value.Split(split);
        foreach (var part in parts)
        {
            if (keys.Any(x => part.Contains(x)))
            {
                var minPart = part.Split("=");
                result += $"{minPart[0]}=*******;";
            }
            else
                result += $"{part};";
        }

        return result;
    }
}

public abstract class ExternalService
{
    public required string Url { get; set; }
}

public class ExternalServices
{
    public required KeycloakService KeycloakService { get; set; }
}

public class KeycloakService : ExternalService
{
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public readonly string Token = "/realms/JotaNunes/protocol/openid-connect/token";
    public readonly string User = "/admin/realms/JotaNunes/users";
}