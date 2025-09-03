using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models;

public class EmpreendimentoTopico : BaseAuditEntity
{
    public required long EmpreendimentoId { get; set; }
    public required long TopicoId { get; set; }

    public required Empreendimento Empreendimento { get; set; }
    public required Topico Topico { get; set; }
}