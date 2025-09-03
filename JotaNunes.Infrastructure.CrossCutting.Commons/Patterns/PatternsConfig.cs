using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using Microsoft.Extensions.DependencyInjection;

namespace JotaNunes.Infrastructure.CrossCutting.Commons.Patterns;

public static class PatternsConfig
{
    public static void RegisterPatterns(this IServiceCollection services)
    {
        services.AddScoped<DefaultResponse>();
        services.AddScoped<INotifications, Notifications>();
    }
}