using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;

namespace JotaNunes.Domain.Services;

public interface IDomainService
{
    ApplicationProvider AppProvider { get; }
    DefaultResponse Response { get; }
    INotifications Notifications { get; }
}