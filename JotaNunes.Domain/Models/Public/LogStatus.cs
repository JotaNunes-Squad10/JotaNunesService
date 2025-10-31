using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class LogStatus : BaseAuditEntity
{
    public required Guid EmpreendimentoId { get; set; }
    public required long Status { get; set; }

    public required EmpreendimentoBase Empreendimento { get; set; }
    public required EmpreendimentoStatus EmpreendimentoStatus { get; set; }
}