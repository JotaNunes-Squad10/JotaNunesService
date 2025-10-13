using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Requests;

public class CreateMaterialRequest : IRequest<DefaultResponse>
{
    public required string Nome { get; set; }
    public required long MarcaId { get; set; }
}