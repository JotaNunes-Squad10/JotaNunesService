using Newtonsoft.Json;

namespace JotaNunes.Application.UseCases.Empreendimentos.Responses;

public class EmpreendimentoBaseResponse()
{
    public required Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public required string Padrao { get; set; }
    public required string Status { get; set; }
    public required int Versao { get; set; }
    [JsonIgnore]
    public Guid UsuarioAlteracaoId { get; set; }
    public string? UsuarioAlteracao { get; set; }
    public required DateTime DataHoraAlteracao { get; set; }
}

public class EmpreendimentoBaseFullResponse()
{
    public required Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public required string Padrao { get; set; }
    public required string Status { get; set; }
    public required int Versao { get; set; }
    [JsonIgnore]
    public Guid UsuarioAlteracaoId { get; set; }
    public string? UsuarioAlteracao { get; set; }
    public required DateTime DataHoraAlteracao { get; set; }

    public List<EmpreendimentoFullResponse>? Empreendimentos { get; set; }
    public List<EmpreendimentoTopicoResponse>? EmpreendimentoTopicos { get; set; }
}

public class EmpreendimentoFullResponse()
{
    public required long Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public required string Padrao { get; set; }
    public required int Versao { get; set; }
}
public class EmpreendimentoTopicoResponse
{
    public required long TopicoId { get; set; }
    public required int Posicao { get; set; }
    public required List<int> Versoes { get; set; }

    public List<TopicoAmbienteResponse>? TopicoAmbientes { get; set; }
    public List<TopicoMaterialResponse>? TopicoMateriais { get; set; }
}

public class TopicoAmbienteResponse
{
    public required long AmbienteId { get; set; }
    public required int Posicao { get; set; }
    public required List<int> Versoes { get; set; }

    public required List<AmbienteItemResponse> AmbienteItens { get; set; }
}

public class TopicoMaterialResponse
{
    public required long MaterialId { get; set; }
    public required List<int> Versoes { get; set; }
}

public class AmbienteItemResponse
{
    public required long ItemId { get; set; }
    public required List<int> Versoes { get; set; }
}