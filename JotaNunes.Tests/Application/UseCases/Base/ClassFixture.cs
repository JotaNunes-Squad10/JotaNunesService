using AutoMapper;
using JotaNunes.Application.AutoMapper;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace JotaNunes.Tests.Application.UseCases.Base;

public class ClassFixture
{
    public IDomainService DomainService { get; }

    public ClassFixture()
    {

        var response = new DefaultResponse();

        var user = Substitute.For<IUser>();
        user.Id.Returns(Guid.NewGuid());
        user.Username.Returns("tester");

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new RequestToDomainMappingProfile(user));
            cfg.AddProfile(new DomainToResponseMappingProfile());
        }, new LoggerFactory());

        DomainService = Substitute.For<IDomainService>();
        DomainService.Mapper.Returns(mapperConfig.CreateMapper());
        DomainService.Notifications.Returns(Substitute.For<INotifications>());
        DomainService.Response.Returns(response);
        DomainService.UnitOfWork.Returns(Substitute.For<IUnitOfWork>());
        DomainService.User.Returns(user);
    }
}