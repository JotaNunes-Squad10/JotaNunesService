namespace JotaNunes.Application.UseCases.Empreendimento.Responses;

public class EmpreendimentoResponse
{
    public required long Id { get; set; }
    public required string Nome { get; set; }
    public required long TamanhoArea { get; set; }
    public required string Localizacao { get; set; }
}