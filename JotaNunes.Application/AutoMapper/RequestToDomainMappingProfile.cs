using AutoMapper;
using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Marca.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models;

namespace JotaNunes.Application.AutoMapper;

public class RequestToDomainMappingProfile : Profile
{
    public RequestToDomainMappingProfile(IUser user)
    {
        CreateMap<CreateEmpreendimentoRequest, Empreendimento>().CreateMapper(user);
        CreateMap<CreateItemRequest, Item>().CreateMapper(user);
        CreateMap<CreateMarcaRequest, Marca>().CreateMapper(user);
        CreateMap<CreateMaterialRequest, Material>().CreateMapper(user);
        CreateMap<CreateTopicoRequest, Topico>().CreateMapper(user);

        CreateMap<UpdateAmbienteRequest, Ambiente>().UpdateMapper(user);
        CreateMap<UpdateTopicoRequest, Topico>().UpdateMapper(user);
    }
}