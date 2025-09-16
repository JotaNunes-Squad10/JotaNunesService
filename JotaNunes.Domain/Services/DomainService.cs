using AutoMapper;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;

namespace JotaNunes.Domain.Services;

public class DomainService(
    ApplicationProvider appProvider,
    DefaultResponse defaultResponse,
    IMapper mapper,
    INotifications notifications,
    IUnitOfWork unitOfWork,
    IUser user)
    : IDomainService
{
    public ApplicationProvider AppProvider => appProvider;
    public DefaultResponse Response => defaultResponse;
    public IMapper Mapper => mapper;
    public INotifications Notifications => notifications;
    public IUnitOfWork UnitOfWork => unitOfWork;
    public IUser User => user;
}