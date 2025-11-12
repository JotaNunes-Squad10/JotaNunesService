using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Handlers;

public class UpdateAmbienteStatusHandler(
    IDomainService domainService,
    ITopicoAmbienteRepository topicoAmbienteRepository,
    IRevisaoAmbienteRepository revisaoAmbienteRepository
) : BaseHandler<RevisaoAmbiente, UpdateAmbienteStatusRequest, AmbienteStatusResponse, IRevisaoAmbienteRepository>(domainService, revisaoAmbienteRepository),
    IRequestHandler<UpdateAmbienteStatusRequest, DefaultResponse>
{
    private readonly IRevisaoAmbienteRepository _revisaoAmbienteRepository = revisaoAmbienteRepository;

    public async Task<DefaultResponse> Handle(UpdateAmbienteStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var topicoAmbiente = await topicoAmbienteRepository.GetByIdAsync(request.AmbienteId);

            if (IsNull(topicoAmbiente))
            {
                AddError("UpdateAmbienteStatusHandler", "Ambiente not found");
                return Response();
            }

            var revisaoAmbiente = await _revisaoAmbienteRepository.GetLastByAmbienteIdAsync(request.AmbienteId);

            if (revisaoAmbiente != null || revisaoAmbiente?.StatusId == request.StatusId)
                return Response("Ambiente status not updated");

            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateAmbienteStatusHandler", "Error updating ambiente status:", e);
            return Response();
        }
    }
}