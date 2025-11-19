using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Requests;

public class PostRevisaoAmbienteRequest : IRequest<DefaultResponse>
{
    public required int AmbienteId { get; set; }
    public required int StatusId { get; set; }
    public string? Observacao { get; set; }
}