using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Handlers;

public class ClearRevisaoAmbienteHandler(
    IDomainService domainService,
    ITopicoAmbienteRepository topicoAmbienteRepository,
    IRevisaoAmbienteRepository revisaoAmbienteRepository
) : BaseHandler<RevisaoAmbiente, ClearRevisaoAmbienteRequest, TopicoAmbienteResponse, IRevisaoAmbienteRepository>(domainService, revisaoAmbienteRepository),
    IRequestHandler<ClearRevisaoAmbienteRequest, DefaultResponse>
{
    private readonly IRevisaoAmbienteRepository _revisaoAmbienteRepository = revisaoAmbienteRepository;

    public async Task<DefaultResponse> Handle(ClearRevisaoAmbienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var topicoAmbiente = await topicoAmbienteRepository.GetByIdAsync(request.AmbienteId);

            if (IsNull(topicoAmbiente))
            {
                AddError(nameof(ClearRevisaoAmbienteHandler), "Ambiente not found.");
                return Response();
            }

            var revisoesAmbiente = await _revisaoAmbienteRepository.GetByAmbienteIdAsync(request.AmbienteId);

            if (revisoesAmbiente is { Count: > 0 })
            {
                revisoesAmbiente.ForEach(ra =>
                {
                    ra.Delete();
                    _revisaoAmbienteRepository.Update(ra);
                });
            }

            await CommitAsync();
            return Response("Ambiente revisions cleared.");
        }
        catch (Exception e)
        {
            AddError(nameof(ClearRevisaoAmbienteHandler), "Error deleting Ambiente revisions:", e);
            return Response();
        }
    }
}