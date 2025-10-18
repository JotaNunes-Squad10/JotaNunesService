using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Requests;

public class CreateAmbienteRequest : IRequest<DefaultResponse>
{
    public required string Nome { get; set; }
    public required long TopicoId { get; set; }
}