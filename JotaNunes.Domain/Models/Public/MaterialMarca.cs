using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class MaterialMarca : BaseAuditEntity
{
    public required long MaterialId { get; set; }
    public required long MarcaId { get; set; }

    public required Material Material { get; set; }
    public required Marca Marca { get; set; }

    public required List<TopicoMaterial> TopicoMateriais { get; set; }
}