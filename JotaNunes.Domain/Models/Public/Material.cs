using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class Material : BaseAuditEntity
{
    public required string Nome { get; set; }
    public required long MarcaId { get; set; }

    public required Marca Marca { get; set; }
    public required List<ItemMaterial> ItemMateriais { get; set; }
}
