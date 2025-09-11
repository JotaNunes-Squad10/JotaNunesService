using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models;

public class Material : BaseAuditEntity
{
    public required string Nome { get; set; }

    public required List<ItemMaterial> ItemMateriais { get; set; }
}
