namespace JotaNunes.Infrastructure.CrossCutting.Commons.Providers;

public class ApplicationProvider
{
    public required ExternalServices ExternalServices { get; set; }
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
    public readonly string Token = "/realms/DevLog/protocol/openid-connect/token";
    public readonly string User = "/admin/realms/DevLog/users";
}