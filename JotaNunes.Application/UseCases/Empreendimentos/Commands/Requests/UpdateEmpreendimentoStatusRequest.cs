using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;

public class UpdateEmpreendimentoStatusRequest : IRequest<DefaultResponse>
{
    public Guid Id { get; set; }
    public long Status { get; set; }
}