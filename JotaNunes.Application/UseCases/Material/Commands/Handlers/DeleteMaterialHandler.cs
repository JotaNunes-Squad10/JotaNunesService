using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Handlers;

public class DeleteMaterialHandler(
    IDomainService domainService,
    IMaterialRepository repository
) : BaseHandler<Domain.Models.Public.Material, DeleteMaterialRequest, MaterialResponse, IMaterialRepository>(domainService, repository),
    IRequestHandler<DeleteMaterialRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(DeleteMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await DeleteAsync(request.Id));
        }
        catch (Exception e)
        {
            AddError("DeleteMaterialHandler", "Error deleting material:", e);
            return Response();
        }
    }
}