using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class EmpreendimentoTopico : BaseAuditEntity
{
    public required Guid EmpreendimentoId { get; set; }
    public required long TopicoId { get; set; }
    public required long Posicao { get; set; }
    public required List<int> Versoes { get; set; }

    public required EmpreendimentoBase Empreendimento { get; set; }
    public required Topico Topico { get; set; }

    public required List<TopicoAmbiente> TopicoAmbientes { get; set; }
    public required List<TopicoMaterial> TopicoMateriais { get; set; }
}