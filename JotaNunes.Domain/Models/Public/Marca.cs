using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class Marca : BaseAuditEntity
{
    public required string Nome { get; set; }

    public required List<MaterialMarca> MaterialMarcas { get; set; }
}