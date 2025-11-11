using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.MaterialMarcas.Commands.Requests;
using JotaNunes.Application.UseCases.MaterialMarcas.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MaterialMarcas.Commands.Handlers;

public class CreateMaterialMarcaHandler(
    IDomainService domainService,
    IMaterialMarcaRepository repository
) : BaseHandler<MaterialMarca, CreateMaterialMarcaRequest, MaterialMarcaResponse, IMaterialMarcaRepository>(domainService, repository),
    IRequestHandler<CreateMaterialMarcaRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateMaterialMarcaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("CreateMaterialMarcaHandler", "Error creating MaterialMarca:", e);
            return Response();
        }
    }
}