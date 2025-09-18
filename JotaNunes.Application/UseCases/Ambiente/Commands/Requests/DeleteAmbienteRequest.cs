using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Requests;

public class DeleteAmbienteRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
}