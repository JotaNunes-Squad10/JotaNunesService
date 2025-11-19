namespace JotaNunes.Application.UseCases.Topico.Responses;

public class RevisaoTopicoResponse
{
    public required long TopicoId { get; set; }
    public required long StatusId { get; set; }
    public string? Observacao { get; set; }
}