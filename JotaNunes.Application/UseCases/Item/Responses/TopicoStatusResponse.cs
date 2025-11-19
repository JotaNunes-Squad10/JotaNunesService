namespace JotaNunes.Application.UseCases.Item.Responses;

public class RevisaoItemResponse
{
    public required long ItemId { get; set; }
    public required long StatusId { get; set; }
    public string? Observacao { get; set; }
}