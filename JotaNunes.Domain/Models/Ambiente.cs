using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models;

public class Ambiente : BaseAuditEntity
{
    public required string Nome { get; set; }
    public required List<AmbienteItem> AmbienteItems { get; set; }
    public required List<TopicoAmbiente> TopicoAmbientes { get; set; }
}