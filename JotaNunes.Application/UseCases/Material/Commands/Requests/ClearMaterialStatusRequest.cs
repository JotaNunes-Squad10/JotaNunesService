using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Requests;

public class ClearMaterialStatusRequest : IRequest<DefaultResponse>
{
    public long MaterialId { get; set; }
}