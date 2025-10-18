using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;

public class UpdateEmpreendimentoRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
    public string? Nome { get; set; }
}
