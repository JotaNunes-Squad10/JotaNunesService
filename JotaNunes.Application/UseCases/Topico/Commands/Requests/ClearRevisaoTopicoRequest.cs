using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Requests;

public class ClearRevisaoTopicoRequest : IRequest<DefaultResponse>
{
    public long TopicoId { get; set; }
}