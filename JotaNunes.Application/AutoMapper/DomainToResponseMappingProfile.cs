using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Authentication.Responses;
using JotaNunes.Application.UseCases.Empreendimento.Responses;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Domain.Models.Base;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.ValueObjects.Base;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;
using UserProfile = JotaNunes.Application.UseCases.Authentication.Responses.Profile;
using Profile = AutoMapper.Profile;

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

        CreateMap<Ambiente, AmbienteResponse>()
            .ForMember(dest => dest.Topico, opt => opt.MapFrom(src => src.Topico));

        CreateMap<Empreendimento, EmpreendimentoResponse>()
            .ForMember(dest => dest.Padrao, opt => opt.MapFrom(src => src.EmpreendimentoPadrao.Nome))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.EmpreendimentoStatus.Descricao));

        CreateMap<Empreendimento, EmpreendimentoFullResponse>()
            .ForMember(dest => dest.Padrao, opt => opt.MapFrom(src => src.EmpreendimentoPadrao.Nome))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.EmpreendimentoStatus.Descricao))
            .ForMember(dest => dest.EmpreendimentoTopicos, opt => opt.MapFrom(src => src.EmpreendimentoTopicos));

        CreateMap<Material, MaterialResponse>()
            .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca.Nome));

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Phone,
                opt => opt.MapFrom(src => src.Attributes.FirstOrDefault(a => a.Name == "phone") != null ? src.Attributes.First(a => a.Name == "phone").Value : string.Empty))
            .ForMember(dest => dest.RequiredActions,
                opt => opt.MapFrom(src => src.UserRequiredActions.Select(ura => ura.Action).ToList()))
            .ForMember(dest => dest.Profiles,
                opt => opt.MapFrom(src => src.UserGroups.Select(ug => new UserProfile(ug)).ToList()))
            .ReverseMap();
    }
}