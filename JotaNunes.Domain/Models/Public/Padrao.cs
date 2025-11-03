using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class Padrao : BaseAuditEntity
{
    public required string Nome { get; set; }

    public required List<Empreendimento> Empreendimentos { get; set; }
}