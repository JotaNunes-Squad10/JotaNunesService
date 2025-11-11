using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MaterialMarcas.Commands.Requests;

public class CreateMaterialMarcaRequest: IRequest<DefaultResponse>
{
    public required long MaterialId { get; set; }
    public required long MarcaId { get; set; }
}