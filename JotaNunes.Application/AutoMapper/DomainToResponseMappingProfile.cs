using JotaNunes.Application.UseCases.Authentication.Responses;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Application.UseCases.MaterialMarcas.Responses;
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

        CreateMap<AmbienteItem, AmbienteItemResponse>();

        CreateMap<EmpreendimentoBase, EmpreendimentoBaseResponse>()
            .ForMember(dest => dest.Nome,        opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.Nome))
            .ForMember(dest => dest.Descricao,   opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.Descricao))
            .ForMember(dest => dest.Localizacao, opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.Localizacao))
            .ForMember(dest => dest.Padrao,      opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.EmpreendimentoPadrao.Nome))
            .ForMember(dest => dest.Status,      opt => opt.MapFrom(src => src.EmpreendimentoStatus.Descricao))
            .ForMember(dest => dest.Versao,      opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.Versao));

        CreateMap<EmpreendimentoBase, EmpreendimentoBaseFullResponse>()
            .ForMember(dest => dest.Nome,                  opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.Nome))
            .ForMember(dest => dest.Descricao,             opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.Descricao))
            .ForMember(dest => dest.Localizacao,           opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.Localizacao))
            .ForMember(dest => dest.Padrao,                opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.EmpreendimentoPadrao.Nome))
            .ForMember(dest => dest.Status,                opt => opt.MapFrom(src => src.EmpreendimentoStatus.Descricao))
            .ForMember(dest => dest.Versao,                opt => opt.MapFrom(src => src.Empreendimentos.MaxBy(x => x.Versao)!.Versao))
            .ForMember(dest => dest.Empreendimentos,       opt => opt.MapFrom(src => src.Empreendimentos))
            .ForMember(dest => dest.EmpreendimentoTopicos, opt => opt.MapFrom(src => src.EmpreendimentoTopicos));

        CreateMap<Empreendimento, EmpreendimentoFullResponse>();

        CreateMap<EmpreendimentoTopico, EmpreendimentoTopicoResponse>()
            .ForMember(dest => dest.TopicoAmbientes, opt => opt.MapFrom(src => src.TopicoAmbientes));

        CreateMap<Marca, MateriaisByMarcaResponse>()
            .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Materiais, opt => opt.MapFrom(src => src.MaterialMarcas.Select(x => x.Material.Nome)));

        CreateMap<MaterialMarca, MaterialMarcaResponse>();

        CreateMap<Material, MarcasByMaterialResponse>()
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Marcas, opt => opt.MapFrom(src => src.MaterialMarcas.Select(x => x.Marca.Nome)));

        CreateMap<TopicoAmbiente, TopicoAmbienteResponse>()
            .ForMember(dest => dest.AmbienteItens, opt => opt.MapFrom(src => src.AmbienteItens));

        CreateMap<TopicoMaterial, TopicoMaterialResponse>();

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