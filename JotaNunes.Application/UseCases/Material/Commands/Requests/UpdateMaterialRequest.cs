using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Requests;

public class UpdateMaterialRequest : IRequest<DefaultResponse>
{
    public string? Nome { get; set; }
    public long? MarcaId { get; set; }
}