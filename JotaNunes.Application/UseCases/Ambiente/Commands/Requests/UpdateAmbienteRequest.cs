using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Requests;

public class UpdateAmbienteRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
    public string? Nome { get; set; }
}
