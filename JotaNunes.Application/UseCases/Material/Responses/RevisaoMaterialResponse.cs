namespace JotaNunes.Application.UseCases.Material.Responses;

public class RevisaoMaterialResponse
{
    public required long MaterialId { get; set; }
    public required long StatusId { get; set; }
    public string? Observacao { get; set; }
}