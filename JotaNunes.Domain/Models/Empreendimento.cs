using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models;

public class Empreendimento : BaseAuditEntity
{
    public required string Nome { get; set; }
    public required long TamanhoArea { get; set; }
    public required string Localizacao { get; set; }
    public required List<EmpreendimentoTopico> EmpreendimentoTopicos { get; set; }
}