using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class EmpreendimentoStatus : BaseEntity
{
    public required string Descricao { get; set; }
    public required List<Empreendimento> Empreendimentos { get; set; }
}