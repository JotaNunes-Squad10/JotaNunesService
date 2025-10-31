using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;

public class UpdateEmpreendimentoStatusRequest : IRequest<DefaultResponse>
{
    public Guid Id { get; set; }
    public int Status { get; set; }
}