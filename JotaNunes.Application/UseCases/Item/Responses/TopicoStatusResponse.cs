namespace JotaNunes.Application.UseCases.Item.Responses;

public class ItemStatusResponse
{
    public required long ItemId { get; set; }
    public required long StatusId { get; set; }
    public string? Observacao { get; set; }
}