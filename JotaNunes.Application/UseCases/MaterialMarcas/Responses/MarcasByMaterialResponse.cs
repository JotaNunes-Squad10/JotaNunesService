namespace JotaNunes.Application.UseCases.MaterialMarcas.Responses;

public class MarcasByMaterialResponse
{
    public required long MaterialId { get; set; }
    public required string Material { get; set; }
    public required List<string> Marcas { get; set; }
}