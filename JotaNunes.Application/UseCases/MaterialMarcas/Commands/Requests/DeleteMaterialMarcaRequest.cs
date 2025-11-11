using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MaterialMarcas.Commands.Requests;

public class DeleteMaterialMarcaRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
}