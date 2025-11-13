using AutoMapper;
using JotaNunes.Application.AutoMapper;
using JotaNunes.Domain.Interfaces;

namespace JotaNunes.Api.Configuration.AutoMapper;

public static class AutoMapperConfig
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddScoped(serviceProvider =>
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullDestinationValues = true;
                mc.AllowNullCollections = true;

                mc.DisableConstructorMapping();

                mc.ValueTransformers.Add<string>(s => s.Trim());

                mc.AddProfile(new DomainToResponseMappingProfile());
                mc.AddProfile(new RequestToDomainMappingProfile(serviceProvider.GetRequiredService<IUser>()));
            }, new LoggerFactory());

            return mapperConfig.CreateMapper(serviceProvider.GetService);
        });
    }
}