using AutoMapper;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;

namespace JotaNunes.Domain.Services;

public interface IDomainService
{
    ApplicationProvider AppProvider { get; }
    DefaultResponse Response { get; }
    IMapper Mapper { get; }
    INotifications Notifications { get; }
    IUnitOfWork UnitOfWork { get; }
    IUser User { get; }
}