namespace JotaNunes.Application.UseCases.MaterialMarcas.Responses;

public class MateriaisByMarcaResponse
{
    public required long MarcaId { get; set; }
    public required string Marca { get; set; }
    public required List<string> Materiais { get; set; }
}