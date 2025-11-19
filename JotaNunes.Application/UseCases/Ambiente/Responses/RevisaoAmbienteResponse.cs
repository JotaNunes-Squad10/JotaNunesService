namespace JotaNunes.Application.UseCases.Ambiente.Responses;

public class RevisaoAmbienteResponse
{
    public required long AmbienteId { get; set; }
    public required long StatusId { get; set; }
    public string? Observacao { get; set; }
}