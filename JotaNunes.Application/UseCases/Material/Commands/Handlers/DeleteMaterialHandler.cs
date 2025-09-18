using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Handlers;

public class DeleteMaterialHandler(
    IDomainService domainService,
    IMaterialRepository repository
) : BaseHandler<Domain.Models.Material, BaseRequest, MaterialResponse, IMaterialRepository>(domainService, repository),
    IRequestHandler<BaseRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(BaseRequest request, CancellationToken cancellationToken)
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