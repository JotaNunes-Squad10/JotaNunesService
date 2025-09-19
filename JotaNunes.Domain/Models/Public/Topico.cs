using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class Topico : BaseAuditEntity
{
    public required string Nome { get; set; }
    public required List<EmpreendimentoTopico> EmpreendimentoTopicos { get; set; }
    public required List<TopicoAmbiente> TopicoAmbientes { get; set; }
}