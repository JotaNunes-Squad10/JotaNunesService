using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Marca.Commands.Requests;

public class CreateMarcaRequest : IRequest<DefaultResponse>
{
    public required string Nome { get; set; }
    public required List<long> MaterialIds { get; set; }
}