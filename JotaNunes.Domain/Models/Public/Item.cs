using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class Item : BaseAuditEntity
{
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required List<EmpreendimentoItem> EmpreendimentoItens { get; set; }
}