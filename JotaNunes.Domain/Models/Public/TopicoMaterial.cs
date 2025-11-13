using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class TopicoMaterial : BaseAuditEntity
{
    public required long TopicoId { get; set; }
    public required long MaterialId { get; set; }
    public required List<int> Versoes { get; set; }

    public required EmpreendimentoTopico EmpreendimentoTopico { get; set; }
    public required MarcaMaterial MarcaMaterial { get; set; }

    public required List<RevisaoMaterial> RevisoesMaterial { get; set; }
}