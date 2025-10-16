using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class EmpreendimentoAmbiente : BaseAuditEntity
{
    public required long EmpreendimentoId { get; set; }
    public required long AmbienteId { get; set; }

    public required Empreendimento Empreendimento { get; set; }
    public required Ambiente Ambiente { get; set; }
}