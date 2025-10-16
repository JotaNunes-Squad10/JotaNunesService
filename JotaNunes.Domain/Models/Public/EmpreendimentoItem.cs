using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class EmpreendimentoItem : BaseAuditEntity
{
    public required long EmpreendimentoId { get; set; }
    public required long ItemId { get; set; }

    public required Empreendimento Empreendimento { get; set; }
    public required Item Item { get; set; }
}