using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MaterialMarcas.Commands.Requests;

public class UpdateMaterialMarcaRequest : IRequest<DefaultResponse>
{
    public required long Id { get; set; }
    public long? MaterialId { get; set; }
    public long? MarcaId { get; set; }
}