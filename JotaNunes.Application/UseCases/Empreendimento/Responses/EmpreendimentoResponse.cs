namespace JotaNunes.Application.UseCases.Empreendimento.Responses;

public class EmpreendimentoBaseResponse()
{
    public required Guid Id { get; set; }
    public required string Status { get; set; }
    public required List<EmpreendimentoResponse> Empreendimentos { get; set; }
}

public class EmpreendimentoResponse()
{
    public required long Id { get; set; }
    public required Guid Guid { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public required long TamanhoArea { get; set; }
    public required string Padrao { get; set; }
    public required long Versao { get; set; }
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
    public required long Versao { get; set; }
    
    public required EmpreendimentoResponse Empreendimento { get; set; }
}