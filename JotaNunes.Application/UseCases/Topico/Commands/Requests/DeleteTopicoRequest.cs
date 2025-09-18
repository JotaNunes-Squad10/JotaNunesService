using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Requests;

public class DeleteTopicoRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
}