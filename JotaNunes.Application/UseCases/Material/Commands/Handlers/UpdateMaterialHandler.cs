using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;
using JotaNunes.Application.UseCases.MarcaMateriais.Responses;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Handlers;

public class UpdateMaterialHandler(
    IDomainService domainService,
    IMaterialRepository repository,
    IMarcaMaterialRepository materialMarcaRepository
) : BaseHandler<Domain.Models.Public.Material, UpdateMaterialRequest, MaterialResponse, IMaterialRepository>(domainService, repository),
    IRequestHandler<UpdateMaterialRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var material = await Repository.GetByIdAsync(request.Id);

            if (IsNull(material)) return Response();

            var marcaMateriais = await materialMarcaRepository.GetByMaterialIdAsync(request.Id);

            marcaMateriais.Where(mm => !request.MarcaIds.Contains(mm.MarcaId)).ToList().ForEach(x =>
            {
                x.Delete();
            });

            request.MarcaIds.Where(marcaId => !marcaMateriais.Any(mm => mm.MarcaId == marcaId)).ToList().ForEach(marcaId =>
            {
                var marcaMaterialRequest = new CreateMarcaMaterialRequest
                {
                    MarcaId = marcaId,
                    MaterialId = material!.Id
                };
                var marcaMaterial = Repository.DomainService.Mapper.Map<MarcaMaterial>(marcaMaterialRequest);

                materialMarcaRepository.InsertAsync(marcaMaterial);
            });

            await CommitAsync();

            var response = await Repository.GetByIdWithMarcasAsync(request.Id);

            return Response(Map<MarcasByMaterialResponse>(response!));
        }
        catch (Exception e)
        {
            AddError(nameof(UpdateMaterialHandler), "Error updating material:", e);
            return Response();
        }
    }
}