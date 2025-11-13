using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Requests;

public class UpdateTopicoStatusRequest : IRequest<DefaultResponse>
{
    public required int TopicoId { get; set; }
    public required int StatusId { get; set; }
    public string? Observacao { get; set; }
}