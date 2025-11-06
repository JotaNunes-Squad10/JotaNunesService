namespace JotaNunes.Application.UseCases.Empreendimentos.Responses;

public class EmpreendimentoBaseResponse()
{
    public required Guid Id { get; set; }
    public required string Status { get; set; }
    public required List<EmpreendimentoResponse> Empreendimentos { get; set; }
}

public class EmpreendimentoResponse()
{
    public required Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public required long TamanhoArea { get; set; }
    public required string Padrao { get; set; }
    public required string Status { get; set; }
    public required int Versao { get; set; }
}

public class EmpreendimentoResultResponse()
{
    public required Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public required long TamanhoArea { get; set; }
    public required string Padrao { get; set; }
    public required string Status { get; set; }
    public required int Versao { get; set; }
}