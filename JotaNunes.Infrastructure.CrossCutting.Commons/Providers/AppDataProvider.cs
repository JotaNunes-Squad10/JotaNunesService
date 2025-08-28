using System.Reflection;

namespace JotaNunes.Infrastructure.CrossCutting.Commons.Providers;

public class AppDataProvider
{
    public static string BaseEndpointName => GetName().ToLower();
    public const string HealthResource = "/health";

    private static Assembly GetAssembly()
        => Assembly.GetExecutingAssembly();

    private static Assembly GetCurrentDomain(string assembly)
        => AppDomain.CurrentDomain.Load($"{GetName()}.{assembly}");

    public static string GetFullName()
        => GetAssembly()?.GetName()?.Name ?? "Unknown";

    public static string GetName()
        => GetFullName().Remove(GetFullName().IndexOf('.'));

    public static Assembly GetApplication()
        => GetCurrentDomain("Application");

    public static Assembly GetDomain()
        => GetCurrentDomain("Domain");

    public static Assembly GetData()
        => GetCurrentDomain("Infrastructure.Data");

    public static Assembly GetIntegration()
        => GetCurrentDomain("Infrastructure.CrossCutting.Integration");
}