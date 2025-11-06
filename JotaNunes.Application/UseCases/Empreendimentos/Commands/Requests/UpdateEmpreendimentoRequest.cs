using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;

public class UpdateEmpreendimentoRequest : IRequest<DefaultResponse>
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public int? TamanhoArea { get; set; }
    public required long Padrao { get; set; }
    [SwaggerIgnore]
    public required long Status { get; set; }
    [SwaggerIgnore]
    public int Versao { get; set; }

    public List<UpdateEmpreendimentoTopicoRequest> EmpreendimentoTopicos { get; set; } = [];
}

public class UpdateEmpreendimentoTopicoRequest
{
    [SwaggerIgnore]
    public required Guid EmpreendimentoId { get; set; }
    public required long TopicoId { get; set; }
    public required int Posicao { get; set; }
    [SwaggerIgnore]
        public List<int> Versoes { get; set; } = [];

    public List<UpdateTopicoAmbienteRequest> TopicoAmbientes { get; set; } = [];
    public List<UpdateTopicoMaterialRequest> TopicoMateriais { get; set; } = [];
}

public class UpdateTopicoAmbienteRequest
{
    [SwaggerIgnore]
    public required long TopicoId { get; set; }
    public required long AmbienteId { get; set; }
    public int? Area { get; set; }
    public required int Posicao { get; set; }
    [SwaggerIgnore]
        public List<int> Versoes { get; set; } = [];

    public List<UpdateAmbienteItemRequest> AmbienteItens { get; set; } = [];
}

public class UpdateTopicoMaterialRequest
{
    [SwaggerIgnore]
    public required long TopicoId { get; set; }
    public required long MaterialId { get; set; }
    [SwaggerIgnore]
        public List<int> Versoes { get; set; } = [];
}

public class UpdateAmbienteItemRequest
{
    [SwaggerIgnore]
    public required long AmbienteId { get; set; }
    public required long ItemId { get; set; }
    [SwaggerIgnore]
        public List<int> Versoes { get; set; } = [];
}