using JotaNunes.Application.UseCases.Topico.Responses;

namespace JotaNunes.Application.UseCases.Ambiente.Responses;

public class AmbienteResponse
{
    public required long Id { get; set; }
    public required string Nome { get; set; }
    public required TopicoResponse Topico { get; set; }
}