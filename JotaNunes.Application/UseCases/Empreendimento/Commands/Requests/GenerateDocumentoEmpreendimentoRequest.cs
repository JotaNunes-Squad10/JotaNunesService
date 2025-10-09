using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;

public class GenerateDocumentoEmpreendimentoRequest : IRequest<DefaultResponse>
{
    public required long Id { get; set; }
}