using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using JotaNunes.Application.UseCases.Topico.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Handlers;

public class ClearRevisaoTopicoHandler(
    IDomainService domainService,
    IEmpreendimentoTopicoRepository empreendimentoTopicoRepository,
    IRevisaoTopicoRepository revisaoTopicoRepository
) : BaseHandler<RevisaoTopico, ClearRevisaoTopicoRequest, RevisaoTopicoResponse, IRevisaoTopicoRepository>(domainService, revisaoTopicoRepository),
    IRequestHandler<ClearRevisaoTopicoRequest, DefaultResponse>
{
    private readonly IRevisaoTopicoRepository _revisaoTopicoRepository = revisaoTopicoRepository;

    public async Task<DefaultResponse> Handle(ClearRevisaoTopicoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var empreendimentoTopico = await empreendimentoTopicoRepository.GetByIdAsync(request.TopicoId);

            if (IsNull(empreendimentoTopico))
            {
                AddError(nameof(ClearRevisaoTopicoHandler), "Topico not found.");
                return Response();
            }

            var revisoesTopico = await _revisaoTopicoRepository.GetByTopicoIdAsync(request.TopicoId);

            if (revisoesTopico is { Count: > 0 })
            {
                revisoesTopico.ForEach(rt =>
                {
                    rt.Delete();
                    _revisaoTopicoRepository.Update(rt);
                });
            }

            await CommitAsync();
            return Response("Topico revisions cleared.");
        }
        catch (Exception e)
        {
            AddError(nameof(ClearRevisaoTopicoHandler), "Error deleting Topico revisions:", e);
            return Response();
        }
    }
}