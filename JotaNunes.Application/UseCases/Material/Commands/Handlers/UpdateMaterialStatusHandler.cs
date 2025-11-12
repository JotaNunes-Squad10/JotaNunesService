using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Handlers;

public class UpdateMaterialStatusHandler(
    IDomainService domainService,
    ITopicoMaterialRepository topicoMaterialRepository,
    IRevisaoMaterialRepository revisaoMaterialRepository
) : BaseHandler<RevisaoMaterial, UpdateMaterialStatusRequest, MaterialStatusResponse, IRevisaoMaterialRepository>(domainService, revisaoMaterialRepository),
    IRequestHandler<UpdateMaterialStatusRequest, DefaultResponse>
{
    private readonly IRevisaoMaterialRepository _revisaoMaterialRepository = revisaoMaterialRepository;

    public async Task<DefaultResponse> Handle(UpdateMaterialStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var topicoMaterial = await topicoMaterialRepository.GetByIdAsync(request.MaterialId);

            if (IsNull(topicoMaterial))
            {
                AddError("UpdateMaterialStatusHandler", "Material not found");
                return Response();
            }

            var revisaoMaterial = await _revisaoMaterialRepository.GetLastByMaterialIdAsync(request.MaterialId);

            if (revisaoMaterial != null || revisaoMaterial?.StatusId == request.StatusId)
                return Response("Material status not updated");

            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateMaterialStatusHandler", "Error updating material status:", e);
            return Response();
        }
    }
}