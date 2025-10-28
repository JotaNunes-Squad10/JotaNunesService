using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class TopicoAmbiente : BaseAuditEntity
{
    public required long TopicoId { get; set; }
    public required long AmbienteId { get; set; }
    public required int Area { get; set; }
    public required int Posicao { get; set; }

    public required EmpreendimentoTopico EmpreendimentoTopico { get; set; }
    public required Ambiente Ambiente { get; set; }

    public required List<AmbienteItem> AmbienteItens { get; set; }
}