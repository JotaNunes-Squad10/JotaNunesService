using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Marca.Commands.Requests;

public class DeleteMarcaRequest : IRequest<DefaultResponse>
{
    public required long Id { get; set; }
}