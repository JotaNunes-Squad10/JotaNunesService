using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Marca.Commands.Requests;
using JotaNunes.Application.UseCases.Marca.Responses;
using JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Marca.Commands.Handlers;

public class CreateMarcaHandler(
    IDomainService domainService,
    IMarcaRepository repository,
    IMarcaMaterialRepository materialMarcaRepository
) : BaseHandler<Domain.Models.Public.Marca, CreateMarcaRequest, MarcaResponse, IMarcaRepository>(domainService, repository),
    IRequestHandler<CreateMarcaRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateMarcaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var marca = await InsertAsync(request);

            request.MaterialIds.ForEach(materialId =>
            {
                var marcaMaterialRequest = new CreateMarcaMaterialRequest
                {
                    MarcaId = marca!.Id,
                    MaterialId = materialId
                };
                var marcaMaterial = Repository.DomainService.Mapper.Map<MarcaMaterial>(marcaMaterialRequest);

                materialMarcaRepository.InsertAsync(marcaMaterial);
            });
            await CommitAsync();

            return Response(marca);
        }
        catch (Exception e)
        {
            AddError("CreateMarcaHandler", "Error creating Marca:", e);
            return Response();
        }
    }
}