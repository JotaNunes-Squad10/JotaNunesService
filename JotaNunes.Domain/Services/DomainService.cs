using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;

namespace JotaNunes.Domain.Services;

public class DomainService(
    ApplicationProvider appProvider,
    DefaultResponse defaultResponse,
    INotifications notifications)
    : IDomainService
{
    public ApplicationProvider AppProvider => appProvider;
    public DefaultResponse Response => defaultResponse;
    public INotifications Notifications => notifications;
}