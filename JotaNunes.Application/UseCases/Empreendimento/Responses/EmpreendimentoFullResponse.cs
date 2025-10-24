namespace JotaNunes.Application.UseCases.Empreendimento.Responses;

public class EmpreendimentoFullResponse()
{
    public required long Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public required long TamanhoArea { get; set; }
    public required string Padrao { get; set; }
    public required string Status { get; set; }
    public required long Versao { get; set; }

    public required List<EmpreendimentoTopicoResponse> EmpreendimentoTopicos { get; set; }
}