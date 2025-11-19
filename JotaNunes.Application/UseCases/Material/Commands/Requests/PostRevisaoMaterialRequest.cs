using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Requests;

public class PostRevisaoMaterialRequest : IRequest<DefaultResponse>
{
    public required long MaterialId { get; set; }
    public required long StatusId { get; set; }
    public string? Observacao { get; set; }
}