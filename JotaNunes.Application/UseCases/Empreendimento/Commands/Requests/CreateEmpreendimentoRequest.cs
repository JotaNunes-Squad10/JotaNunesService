using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;

public class CreateEmpreendimentoRequest : IRequest<DefaultResponse>
{
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required int TamanhoArea { get; set; }
    public required string Localizacao { get; set; }
    public required long Padrao { get; set; }
    public required long Status { get; set; }
    [SwaggerIgnore]
    public required long Versao { get; set; }

    public required List<EmpreendimentoTopicoRequest> EmpreendimentoTopicos { get; set; }
}

public class EmpreendimentoTopicoRequest
{
    [SwaggerIgnore]
    public required long EmpreendimentoId { get; set; }
    public required long TopicoId { get; set; }
    public required long Posicao { get; set; }

    public required List<TopicoAmbienteRequest> TopicoAmbientes { get; set; }
    public required List<TopicoMaterialRequest> TopicoMateriais { get; set; }
}

public class TopicoAmbienteRequest
{
    [SwaggerIgnore]
    public required long TopicoId { get; set; }
    public required long AmbienteId { get; set; }
    public required long Area { get; set; }
    public required long Posicao { get; set; }

    public required List<AmbienteItemRequest> AmbienteItens { get; set; }
}

public class TopicoMaterialRequest
{
    [SwaggerIgnore]
    public required long TopicoId { get; set; }
    public required long MaterialId { get; set; }
}

public class AmbienteItemRequest
{
    [SwaggerIgnore]
    public required long AmbienteId { get; set; }
    public required long ItemId { get; set; }
}
