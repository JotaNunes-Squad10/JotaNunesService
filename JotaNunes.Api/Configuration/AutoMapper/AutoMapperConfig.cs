using AutoMapper;
using AutoMapper.Internal;
using JotaNunes.Application.AutoMapper;
using JotaNunes.Domain.Interfaces;

namespace JotaNunes.Api.Configuration.AutoMapper;

public static class AutoMapperConfig
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddSingleton(serviceProvider => new MapperConfiguration(mc =>
        {
            mc.AllowNullDestinationValues = true;
            mc.AllowNullCollections = true;

            mc.DisableConstructorMapping();

            mc.Internal().ForAllMaps
            (
                (mapType, mapperExpression) =>
                {
                    mapperExpression.BeforeMap<TrimAllStringProperty>();
                }
            );
            var user = serviceProvider.GetService<IUser>();
            mc.AddProfile(new DomainToResponseMappingProfile());
            mc.AddProfile(new RequestToDomainMappingProfile(user));
        }, new LoggerFactory()).CreateMapper());
    }
}

public class TrimAllStringProperty : IMappingAction<object, object>
{
    public void Process(object source, object destination, ResolutionContext context)
    {
        var stringProperties = destination.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
        foreach (var stringProperty in stringProperties)
        {
            string? currentValue = (string)stringProperty.GetValue(destination, null);
            if (currentValue != null)
                stringProperty.SetValue(destination, currentValue.Trim(), null);
        }
    }
}