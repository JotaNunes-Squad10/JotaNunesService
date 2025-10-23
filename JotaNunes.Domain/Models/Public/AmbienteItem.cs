using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class AmbienteItem : BaseAuditEntity
{
    public required long AmbienteId { get; set; }
    public required long ItemId { get; set; }

    public required TopicoAmbiente TopicoAmbiente { get; set; }
    public required Item Item { get; set; }
}