using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Handlers;

public class PostRevisaoMaterialHandler(
    IDomainService domainService,
    ITopicoMaterialRepository topicoMaterialRepository,
    IRevisaoMaterialRepository revisaoMaterialRepository
) : BaseHandler<RevisaoMaterial, PostRevisaoMaterialRequest, RevisaoMaterialResponse, IRevisaoMaterialRepository>(domainService, revisaoMaterialRepository),
    IRequestHandler<PostRevisaoMaterialRequest, DefaultResponse>
{
    private readonly IRevisaoMaterialRepository _revisaoMaterialRepository = revisaoMaterialRepository;

    public async Task<DefaultResponse> Handle(PostRevisaoMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var topicoMaterial = await topicoMaterialRepository.GetByIdAsync(request.MaterialId);

            if (IsNull(topicoMaterial))
            {
                AddError(nameof(PostRevisaoMaterialHandler), "Material not found.");
                return Response();
            }

            var revisaoMaterial = await _revisaoMaterialRepository.GetByMaterialIdAsync(request.MaterialId);

            if (revisaoMaterial is { Count: > 0 }
                && revisaoMaterial.LastOrDefault()!.StatusId == request.StatusId
                && revisaoMaterial.LastOrDefault()!.Observacao == request.Observacao)
                return Response("The status of the Material has not changed.");

            revisaoMaterial.ForEach(rm =>
            {
                rm.Delete();
                _revisaoMaterialRepository.Update(rm);
            });

            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError(nameof(PostRevisaoMaterialHandler), "Error creating revision for Material:", e);
            return Response();
        }
    }
}