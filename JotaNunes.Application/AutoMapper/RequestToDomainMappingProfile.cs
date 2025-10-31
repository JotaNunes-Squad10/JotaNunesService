using AutoMapper;
using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Marca.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Application.AutoMapper;

public class RequestToDomainMappingProfile : Profile
{
    public RequestToDomainMappingProfile(IUser user)
    {
        CreateMap<CreateAmbienteRequest, Ambiente>().CreateMapper(user);
        CreateMap<CreateItemRequest, Item>().CreateMapper(user);
        CreateMap<CreateMarcaRequest, Marca>().CreateMapper(user);
        CreateMap<CreateMaterialRequest, Material>().CreateMapper(user);
        CreateMap<CreateTopicoRequest, Topico>().CreateMapper(user);

        CreateMap<AmbienteItemRequest, AmbienteItem>().CreateMapper(user);
        CreateMap<EmpreendimentoTopicoRequest, EmpreendimentoTopico>().CreateMapper(user);
        CreateMap<TopicoAmbienteRequest, TopicoAmbiente>().CreateMapper(user);
        CreateMap<TopicoMaterialRequest, TopicoMaterial>().CreateMapper(user);

        CreateMap<UpdateAmbienteRequest, Ambiente>().UpdateMapper(user);
        CreateMap<UpdateItemRequest, Item>().UpdateMapper(user);
        CreateMap<UpdateMarcaRequest, Marca>().UpdateMapper(user);
        CreateMap<UpdateMaterialRequest, Material>().UpdateMapper(user);
        CreateMap<UpdateTopicoRequest, Topico>().UpdateMapper(user);

        CreateMap<CreateEmpreendimentoRequest, EmpreendimentoBase>()
            .ForMember(dest => dest.Status,        opt => opt.MapFrom(_ => (long)Status.Pendente))
            .ForMember(dest => dest.Empreendimentos, opt => opt.MapFrom(_ => new List<Empreendimento>()))
            .ForMember(dest => dest.LogsStatus,      opt => opt.MapFrom(_ => new List<LogStatus>()))
            .CreateMapper(user);

        CreateMap<CreateEmpreendimentoRequest, Empreendimento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EmpreendimentoBase, opt => opt.Ignore())
            .ForMember(dest => dest.EmpreendimentoPadrao, opt => opt.Ignore())
            .ForMember(dest => dest.EmpreendimentoTopicos, opt => opt.Ignore())
            .ForMember(dest => dest.Guid,   opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items.ContainsKey("Guid")   ? (Guid)ctx.Items["Guid"]   : Guid.Empty))
            .ForMember(dest => dest.Versao, opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items.ContainsKey("Versao") ? (long)ctx.Items["Versao"] : 1))
            .CreateMapper(user);
    }
}