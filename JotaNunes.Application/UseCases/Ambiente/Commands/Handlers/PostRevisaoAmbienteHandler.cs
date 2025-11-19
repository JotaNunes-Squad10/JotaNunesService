using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Handlers;

public class PostRevisaoAmbienteHandler(
    IDomainService domainService,
    ITopicoAmbienteRepository topicoAmbienteRepository,
    IRevisaoAmbienteRepository revisaoAmbienteRepository
) : BaseHandler<RevisaoAmbiente, PostRevisaoAmbienteRequest, RevisaoAmbienteResponse, IRevisaoAmbienteRepository>(domainService, revisaoAmbienteRepository),
    IRequestHandler<PostRevisaoAmbienteRequest, DefaultResponse>
{
    private readonly IRevisaoAmbienteRepository _revisaoAmbienteRepository = revisaoAmbienteRepository;

    public async Task<DefaultResponse> Handle(PostRevisaoAmbienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var topicoAmbiente = await topicoAmbienteRepository.GetByIdAsync(request.AmbienteId);

            if (IsNull(topicoAmbiente))
            {
                AddError(nameof(PostRevisaoAmbienteHandler), "Ambiente not found.");
                return Response();
            }

            var revisaoAmbiente = await _revisaoAmbienteRepository.GetByAmbienteIdAsync(request.AmbienteId);

            if (revisaoAmbiente is { Count: > 0 }
                && revisaoAmbiente.LastOrDefault()!.StatusId == request.StatusId
                && revisaoAmbiente.LastOrDefault()!.Observacao == request.Observacao)
                return Response("The status of the Ambiente has not changed.");

            revisaoAmbiente.ForEach(ra =>
            {
                ra.Delete();
                _revisaoAmbienteRepository.Update(ra);
            });

            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError(nameof(PostRevisaoAmbienteHandler), "Error creating revision for Ambiente:", e);
            return Response();
        }
    }
}