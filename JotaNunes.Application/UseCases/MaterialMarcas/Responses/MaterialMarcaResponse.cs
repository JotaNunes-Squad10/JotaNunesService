using JotaNunes.Application.UseCases.Marca.Responses;
using JotaNunes.Application.UseCases.Material.Responses;

namespace JotaNunes.Application.UseCases.MaterialMarcas.Responses;

public class MaterialMarcaResponse
{
    public required long Id { get; set; }
    public required MaterialResponse Material { get; set; }
    public required MarcaResponse Marca { get; set; }
}