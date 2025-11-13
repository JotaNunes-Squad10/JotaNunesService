using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class RevisaoAmbiente : BaseAuditEntity
{
    public required long AmbienteId { get; set; }
    public required long StatusId { get; set; }
    public required string Observacao { get; set; }

    public required TopicoAmbiente TopicoAmbiente { get; set; }
    public required StatusRevisao StatusRevisao { get; set; }
}