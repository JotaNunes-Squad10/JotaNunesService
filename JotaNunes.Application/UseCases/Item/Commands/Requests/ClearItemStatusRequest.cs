using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Requests;

public class ClearItemStatusRequest : IRequest<DefaultResponse>
{
    public long ItemId { get; set; }
}