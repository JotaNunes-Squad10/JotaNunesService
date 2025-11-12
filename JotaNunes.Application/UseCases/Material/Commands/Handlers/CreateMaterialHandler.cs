using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Handlers;

public class CreateMaterialHandler(
    IDomainService domainService,
    IMaterialRepository repository,
    IMarcaMaterialRepository materialMarcaRepository
) : BaseHandler<Domain.Models.Public.Material, CreateMaterialRequest, MaterialResponse, IMaterialRepository>(domainService, repository),
    IRequestHandler<CreateMaterialRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var material = await InsertAsync(request);

            if (IsNull(material)) return Response();

            request.MarcaIds.ForEach(marcaId =>
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

            return Response(material);
        }
        catch (Exception e)
        {
            AddError("CreateMaterialmHandler", "Error creating material:", e);
            return Response();
        }
    }
}