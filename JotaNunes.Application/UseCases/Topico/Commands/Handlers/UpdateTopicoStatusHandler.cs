using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using JotaNunes.Application.UseCases.Topico.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Handlers;

public class UpdateTopicoStatusHandler(
    IDomainService domainService,
    IEmpreendimentoTopicoRepository empreendimentoTopicoRepository,
    IRevisaoTopicoRepository revisaoTopicoRepository
) : BaseHandler<RevisaoTopico, UpdateTopicoStatusRequest, TopicoStatusResponse, IRevisaoTopicoRepository>(domainService, revisaoTopicoRepository),
    IRequestHandler<UpdateTopicoStatusRequest, DefaultResponse>
{
    private readonly IRevisaoTopicoRepository _revisaoTopicoRepository = revisaoTopicoRepository;

    public async Task<DefaultResponse> Handle(UpdateTopicoStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var empreendimentoTopico = await empreendimentoTopicoRepository.GetByIdAsync(request.TopicoId);

            if (IsNull(empreendimentoTopico))
            {
                AddError("UpdateTopicoStatusHandler", "Topico not found");
                return Response();
            }

            var revisaoTopico = await _revisaoTopicoRepository.GetLastByTopicoIdAsync(request.TopicoId);

            if (revisaoTopico != null || revisaoTopico?.StatusId == request.StatusId)
                return Response("Topico status not updated");

            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateTopicoStatusHandler", "Error updating topico status:", e);
            return Response();
        }
    }
}