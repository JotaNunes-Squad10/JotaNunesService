using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Requests;

public class ClearRevisaoAmbienteRequest : IRequest<DefaultResponse>
{
    public long AmbienteId { get; set; }
}