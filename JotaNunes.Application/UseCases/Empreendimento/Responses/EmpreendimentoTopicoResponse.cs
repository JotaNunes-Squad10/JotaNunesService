namespace JotaNunes.Application.UseCases.Empreendimento.Responses;

public class EmpreendimentoTopicoResponse
{
    public required long EmpreendimentoId { get; set; }
    public required long TopicoId { get; set; }
    public required long Posicao { get; set; }
}