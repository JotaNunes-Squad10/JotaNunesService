using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;
using JotaNunes.Application.UseCases.MarcaMateriais.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MarcaMateriais.Commands.Handlers;

public class DeleteMarcaMaterialHandler(
    IDomainService domainService,
    IMarcaMaterialRepository repository
) : BaseHandler<MarcaMaterial, DeleteMarcaMaterialRequest, MarcaMaterialResponse, IMarcaMaterialRepository>(domainService, repository),
    IRequestHandler<DeleteMarcaMaterialRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(DeleteMarcaMaterialRequest request, CancellationToken cancellationToken)
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