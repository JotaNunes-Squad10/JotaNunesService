using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Handlers;

public class ClearRevisaoMaterialHandler(
    IDomainService domainService,
    ITopicoMaterialRepository topicoMaterialRepository,
    IRevisaoMaterialRepository revisaoMaterialRepository
) : BaseHandler<RevisaoMaterial, ClearRevisaoMaterialRequest, RevisaoMaterialResponse, IRevisaoMaterialRepository>(domainService, revisaoMaterialRepository),
    IRequestHandler<ClearRevisaoMaterialRequest, DefaultResponse>
{
    private readonly IRevisaoMaterialRepository _revisaoMaterialRepository = revisaoMaterialRepository;

    public async Task<DefaultResponse> Handle(ClearRevisaoMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var topicoMaterial = await topicoMaterialRepository.GetByIdAsync(request.MaterialId);

            if (IsNull(topicoMaterial))
            {
                AddError(nameof(ClearRevisaoMaterialHandler), "Material not found.");
                return Response();
            }

            var revisoesMaterial = await _revisaoMaterialRepository.GetByMaterialIdAsync(request.MaterialId);

            if (revisoesMaterial is { Count: > 0 })
            {
                revisoesMaterial.ForEach(rm =>
                {
                    rm.Delete();
                    _revisaoMaterialRepository.Update(rm);
                });
            }

            await CommitAsync();
            return Response("Material revisions cleared.");
        }
        catch (Exception e)
        {
            AddError(nameof(ClearRevisaoMaterialHandler), "Error deleting Material revisions:", e);
            return Response();
        }
    }
}