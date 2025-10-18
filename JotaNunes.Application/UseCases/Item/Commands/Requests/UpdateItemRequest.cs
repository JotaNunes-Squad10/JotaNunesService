using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Requests;

public class UpdateItemRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
}