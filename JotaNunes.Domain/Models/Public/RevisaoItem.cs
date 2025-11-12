using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class RevisaoItem : BaseAuditEntity
{
    public required long ItemId { get; set; }
    public required long StatusId { get; set; }
    public required string Observacao { get; set; }

    public required AmbienteItem AmbienteItem { get; set; }
    public required StatusRevisao StatusRevisao { get; set; }
}