using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;

public class GenerateDocumentoEmpreendimentoRequest : IRequest<DefaultResponse>
{
    public required Guid Id { get; set; }
    public int? EmpreendimentoVersion { get; set; }
    public int? DocumentVersion { get; set; }
}