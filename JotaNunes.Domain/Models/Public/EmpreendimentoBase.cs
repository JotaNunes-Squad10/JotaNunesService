using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class EmpreendimentoBase : BaseAuditEntity
{
    public new Guid Id { get; set; }
    public required long Status { get; set; }

    public required EmpreendimentoStatus EmpreendimentoStatus { get; set; }

    public required List<Empreendimento> Empreendimentos { get; set; }
    public required List<LogStatus> LogsStatus { get; set; }
}