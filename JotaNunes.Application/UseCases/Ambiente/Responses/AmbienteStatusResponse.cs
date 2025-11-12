namespace JotaNunes.Application.UseCases.Ambiente.Responses;

public class AmbienteStatusResponse
{
    public required long AmbienteId { get; set; }
    public required long StatusId { get; set; }
    public string? Observacao { get; set; }
}