using JotaNunes.Application.UseCases.Marca.Responses;
using JotaNunes.Application.UseCases.Material.Responses;

namespace JotaNunes.Application.UseCases.MarcaMateriais.Responses;

public class MarcaMaterialResponse
{
    public required long Id { get; set; }
    public required MaterialResponse Material { get; set; }
    public required MarcaResponse Marca { get; set; }
}