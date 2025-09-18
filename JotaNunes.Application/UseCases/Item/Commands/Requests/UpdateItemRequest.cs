using JotaNunes.Application.UseCases.Base.Commands;

namespace JotaNunes.Application.UseCases.Item.Commands.Requests;

public class UpdateItemRequest : BaseRequest
{
    public string? Nome { get; set; }
}