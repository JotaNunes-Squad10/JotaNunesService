using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.MaterialMarcas.Commands.Requests;
using JotaNunes.Application.UseCases.MaterialMarcas.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MaterialMarcas.Commands.Handlers;

public class UpdateMaterialMarcaHandler(
    IDomainService domainService,
    IMaterialMarcaRepository repository
) : BaseHandler<MaterialMarca, UpdateMaterialMarcaRequest, MaterialMarcaResponse, IMaterialMarcaRepository>(domainService, repository),
    IRequestHandler<UpdateMaterialMarcaRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateMaterialMarcaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await UpdateAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateMaterialMarcaHandler", "Error updating MaterialMarca:", e);
            return Response();
        }
    }
}