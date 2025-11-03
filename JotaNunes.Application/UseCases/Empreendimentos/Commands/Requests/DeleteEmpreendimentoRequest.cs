using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;

public class DeleteEmpreendimentoRequest : IRequest<DefaultResponse>
{
    public Guid Id { get; set; }
}
