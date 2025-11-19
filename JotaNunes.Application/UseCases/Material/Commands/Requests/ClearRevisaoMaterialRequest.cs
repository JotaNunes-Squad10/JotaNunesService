using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Requests;

public class ClearRevisaoMaterialRequest : IRequest<DefaultResponse>
{
    public long MaterialId { get; set; }
}