using AutoMapper;
using JotaNunes.Domain.Models.Base;
using JotaNunes.Domain.ValueObjects.Base;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;

namespace JotaNunes.Application.AutoMapper;

public class DomainToResponseMappingProfile : Profile
{
    public DomainToResponseMappingProfile()
    {
        var sufix = "Response";
        var excludeTypes = new List<Type>();

        var responses = new[] { AppDataProvider.GetApplication(), AppDataProvider.GetIntegration() }
            .SelectMany(a => a.DefinedTypes)
            .Where(x =>
                !x.IsInterface
                && !x.IsAbstract
                && !x.IsEnum
                && x.IsVisible
                && x.IsPublic
                && x.UnderlyingSystemType.Name.Contains(sufix))
            .Select(x => x.UnderlyingSystemType)
            .Distinct()
            .ToList();

        if (responses.Any())
        {
            var models = AppDataProvider.GetDomain()
                .DefinedTypes
                .Where(x =>
                    !x.IsInterface
                    && !x.IsAbstract
                    && !x.IsEnum
                    && x.IsVisible
                    && x.IsPublic
                    && !excludeTypes.Contains(x.UnderlyingSystemType)
                    && x.BaseType != null
                    && (x.BaseType.Name.Equals(nameof(BaseEntity))
                        || x.BaseType.Name.Equals(nameof(BaseAuditEntity))
                        || x.BaseType.Name.Equals(nameof(Object))
                        || x.BaseType.Name.Equals(nameof(ValueObject)))
                    && responses.Select(r => r.Name)
                        .Contains(x.UnderlyingSystemType.Name + sufix))
                        .Select(x => x.UnderlyingSystemType)
                .ToList();

            if (models.Any())
            {
                foreach (var model in models)
                {
                    var response = responses.FirstOrDefault(r => r.Name.Equals(model.Name + sufix));
                    if (response is not null)
                        CreateMap(model, response).ReverseMap();
                }
            }
        }
    }
}