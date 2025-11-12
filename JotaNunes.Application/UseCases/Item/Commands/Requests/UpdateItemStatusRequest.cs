using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Requests;

public class UpdateItemStatusRequest : IRequest<DefaultResponse>
{
    public required long ItemId { get; set; }
    public required long StatusId { get; set; }
    public string? Observacao { get; set; }
}