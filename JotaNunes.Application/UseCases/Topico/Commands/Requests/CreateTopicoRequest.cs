using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Requests;

public class CreateTopicoRequest : IRequest<DefaultResponse>
{
    public required string Nome { get; set; }
}