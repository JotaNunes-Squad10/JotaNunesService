using AutoMapper;
using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;
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

        CreateMap<CreateAmbienteItemRequest, AmbienteItem>().CreateMapper(user);
        CreateMap<CreateEmpreendimentoTopicoRequest, EmpreendimentoTopico>().CreateMapper(user);
        CreateMap<CreateTopicoAmbienteRequest, TopicoAmbiente>().CreateMapper(user);
        CreateMap<CreateTopicoMaterialRequest, TopicoMaterial>().CreateMapper(user);

        CreateMap<UpdateAmbienteRequest, Ambiente>().UpdateMapper(user);
        CreateMap<UpdateItemRequest, Item>().UpdateMapper(user);
        CreateMap<UpdateMarcaRequest, Marca>().UpdateMapper(user);
        CreateMap<UpdateMaterialRequest, Material>().UpdateMapper(user);
        CreateMap<UpdateTopicoRequest, Topico>().UpdateMapper(user);

        CreateMap<UpdateAmbienteItemRequest, AmbienteItem>().CreateMapper(user);
        CreateMap<UpdateEmpreendimentoTopicoRequest, EmpreendimentoTopico>().CreateMapper(user);
        CreateMap<UpdateTopicoAmbienteRequest, TopicoAmbiente>().CreateMapper(user);
        CreateMap<UpdateTopicoMaterialRequest, TopicoMaterial>().CreateMapper(user);

        // ===== Create Empreendimento ======

        CreateMap<CreateEmpreendimentoRequest, EmpreendimentoBase>().CreateMapper(user);

        CreateMap<CreateEmpreendimentoRequest, Empreendimento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EmpreendimentoBase, opt => opt.Ignore())
            .ForMember(dest => dest.EmpreendimentoPadrao, opt => opt.Ignore())
            .ForMember(dest => dest.Guid,   opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items.ContainsKey("Guid")   ? (Guid)ctx.Items["Guid"]  : Guid.Empty))
            .ForMember(dest => dest.Versao, opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items.ContainsKey("Versao") ? (int)ctx.Items["Versao"] : 1))
            .CreateMapper(user);

        CreateMap<LogStatusRequest, LogStatus>().CreateMapper(user);

        // ===== Update Empreendimento =====

        CreateMap<UpdateEmpreendimentoRequest, EmpreendimentoBase>()
            .ForMember(dest => dest.EmpreendimentoTopicos, opt => opt.Ignore())
            .CreateMapper(user);

        CreateMap<UpdateEmpreendimentoRequest, Empreendimento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EmpreendimentoBase, opt => opt.Ignore())
            .ForMember(dest => dest.EmpreendimentoPadrao, opt => opt.Ignore())
            .ForMember(dest => dest.Guid,   opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items.ContainsKey("Guid")   ? (Guid)ctx.Items["Guid"]  : Guid.Empty))
            .ForMember(dest => dest.Versao, opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items.ContainsKey("Versao") ? (int)ctx.Items["Versao"] : 1))
            .CreateMapper(user);

        CreateMap<UpdateEmpreendimentoTopicoRequest, EmpreendimentoTopico>()
            .ForMember(dest => dest.TopicoAmbientes, opt => opt.Ignore())
            .ForMember(dest => dest.TopicoMateriais, opt => opt.Ignore())
            .CreateMapper(user);

        CreateMap<UpdateTopicoAmbienteRequest, TopicoAmbiente>()
            .ForMember(dest => dest.AmbienteItens, opt => opt.Ignore())
            .CreateMapper(user);
    }
}