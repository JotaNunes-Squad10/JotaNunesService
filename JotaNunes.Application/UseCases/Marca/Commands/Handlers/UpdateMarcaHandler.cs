using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Marca.Commands.Requests;
using JotaNunes.Application.UseCases.Marca.Responses;
using JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;
using JotaNunes.Application.UseCases.MarcaMateriais.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Marca.Commands.Handlers;

public class UpdateMarcaHandler(
    IDomainService domainService,
    IMarcaRepository repository,
    IMarcaMaterialRepository materialMarcaRepository
) : BaseHandler<Domain.Models.Public.Marca, UpdateMarcaRequest, MarcaResponse, IMarcaRepository>(domainService, repository),
    IRequestHandler<UpdateMarcaRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateMarcaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var marca = await Repository.GetByIdAsync(request.Id);

            if (IsNull(marca)) return Response();

            await UpdateAsync(request);

            var marcaMateriais = await materialMarcaRepository.GetByMarcaIdAsync(request.Id);

            marcaMateriais.Where(x => !request.MaterialIds.Contains(x.MaterialId)).ToList().ForEach(x =>
            {
                x.Delete();
            });

            request.MaterialIds.Where(materialId => !marcaMateriais.Any(mm => mm.MaterialId == materialId)).ToList().ForEach(materialId =>
            {
                var marcaMaterialRequest = new CreateMarcaMaterialRequest
                {
                    MarcaId = request.Id,
                    MaterialId = materialId
                };
                var marcaMaterial = Repository.DomainService.Mapper.Map<MarcaMaterial>(marcaMaterialRequest);

                materialMarcaRepository.InsertAsync(marcaMaterial);
            });

            await CommitAsync();

            var response = await Repository.GetByIdWithMateriaisAsync(request.Id);

            return Response(Map<MateriaisByMarcaResponse>(response!));
        }
        catch (Exception e)
        {
            AddError(nameof(UpdateMarcaHandler), "Error updating marca:", e);
            return Response();
        }
    }
}