using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class RevisaoTopico : BaseAuditEntity
{
    public required long TopicoId { get; set; }
    public required long StatusId { get; set; }
    public required string Observacao { get; set; }

    public required EmpreendimentoTopico EmpreendimentoTopico { get; set; }
    public required StatusRevisao StatusRevisao { get; set; }
}