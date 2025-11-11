using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.MaterialMarcas.Commands.Requests;
using JotaNunes.Application.UseCases.MaterialMarcas.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MaterialMarcas.Commands.Handlers;

public class DeleteMaterialMarcaHandler(
    IDomainService domainService,
    IMaterialMarcaRepository repository
) : BaseHandler<MaterialMarca, DeleteMaterialMarcaRequest, MaterialMarcaResponse, IMaterialMarcaRepository>(domainService, repository),
    IRequestHandler<DeleteMaterialMarcaRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(DeleteMaterialMarcaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await DeleteAsync(request.Id));
        }
        catch (Exception e)
        {
            AddError("DeleteMaterialMarcaHandler", "Error deleting MaterialMarca:", e);
            return Response();
        }
    }
}