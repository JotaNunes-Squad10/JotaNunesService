using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class EmpreendimentoMaterial : BaseAuditEntity
{
    public required long EmpreendimentoId { get; set; }
    public required long MaterialId { get; set; }

    public required Empreendimento Empreendimento { get; set; }
    public required Material Material { get; set; }
}