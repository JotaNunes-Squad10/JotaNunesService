using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Requests;

public class UpdateMaterialStatusRequest : IRequest<DefaultResponse>
{
    public required int MaterialId { get; set; }
    public required int StatusId { get; set; }
    public string? Observacao { get; set; }
}