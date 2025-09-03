using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Requests;

public class CreateItemRequest : IRequest<DefaultResponse>
{
    public required string Nome { get; set; }
}