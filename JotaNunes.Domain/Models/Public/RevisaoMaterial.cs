using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class RevisaoMaterial : BaseAuditEntity
{
    public required long MaterialId { get; set; }
    public required long StatusId { get; set; }
    public required string Observacao { get; set; }

    public required TopicoMaterial TopicoMaterial { get; set; }
    public required StatusRevisao StatusRevisao { get; set; }
}