namespace JotaNunes.Application.UseCases.Item.Responses;

public class ItemResponse
{
    public required long Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
}