using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class Ambiente : BaseAuditEntity
{
    public required string Nome { get; set; }
    public required List<EmpreendimentoAmbiente> EmpreendimentoAmbientes { get; set; }
}