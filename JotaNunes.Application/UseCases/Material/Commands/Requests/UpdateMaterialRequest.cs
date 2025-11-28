using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Requests;

public class UpdateMaterialRequest : IRequest<DefaultResponse>
{
    public required long Id { get; set; }
    public required string Nome { get; set; }
    public required List<long> MarcaIds { get; set; }
}