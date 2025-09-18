using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Requests;

public class DeleteItemRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
}