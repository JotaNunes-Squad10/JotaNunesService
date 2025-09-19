using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class ItemMaterial : BaseAuditEntity
{
    public required long ItemId { get; set; }
    public required long MaterialId { get; set; }
    public long? MarcaId { get; set; }

    public required Item Item { get; set; }
    public required Material Material { get; set; }
    public Marca? Marca { get; set; }
}