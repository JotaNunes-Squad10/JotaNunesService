using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Marca.Commands.Requests;

public class UpdateMarcaRequest : IRequest<DefaultResponse>
{
    public string? Nome { get; set; }
}