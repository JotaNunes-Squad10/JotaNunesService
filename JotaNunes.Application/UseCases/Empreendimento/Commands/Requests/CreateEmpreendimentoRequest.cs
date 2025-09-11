using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;

public class CreateEmpreendimentoRequest : IRequest<DefaultResponse>
{
    public required string Nome { get; set; }
    public required long TamanhoArea { get; set; }
    public required string Localizacao { get; set; }
}