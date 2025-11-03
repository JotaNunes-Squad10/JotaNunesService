using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;

public class GenerateDocumentoEmpreendimentoRequest : IRequest<DefaultResponse>
{
    public required Guid Id { get; set; }
    public long? Version { get; set; }
}
