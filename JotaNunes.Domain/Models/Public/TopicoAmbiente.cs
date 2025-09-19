using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class TopicoAmbiente : BaseAuditEntity
{
    public required long TopicoId { get; set; }
    public required long AmbienteId { get; set; }

    public required Topico Topico { get; set; }
    public required Ambiente Ambiente { get; set; }
}