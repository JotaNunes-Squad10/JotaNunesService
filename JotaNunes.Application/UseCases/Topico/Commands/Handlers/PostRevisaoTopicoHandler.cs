using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using JotaNunes.Application.UseCases.Topico.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Handlers;

public class PostRevisaoTopicoHandler(
    IDomainService domainService,
    IEmpreendimentoTopicoRepository empreendimentoTopicoRepository,
    IRevisaoTopicoRepository revisaoTopicoRepository
) : BaseHandler<RevisaoTopico, PostRevisaoTopicoRequest, RevisaoTopicoResponse, IRevisaoTopicoRepository>(domainService, revisaoTopicoRepository),
    IRequestHandler<PostRevisaoTopicoRequest, DefaultResponse>
{
    private readonly IRevisaoTopicoRepository _revisaoTopicoRepository = revisaoTopicoRepository;

    public async Task<DefaultResponse> Handle(PostRevisaoTopicoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var empreendimentoTopico = await empreendimentoTopicoRepository.GetByIdAsync(request.TopicoId);

            if (IsNull(empreendimentoTopico))
            {
                AddError(nameof(PostRevisaoTopicoHandler), "Topico not found.");
                return Response();
            }

            var revisaoTopico = await _revisaoTopicoRepository.GetByTopicoIdAsync(request.TopicoId);

            if (revisaoTopico is { Count: > 0 }
                && revisaoTopico.LastOrDefault()!.StatusId == request.StatusId
                && revisaoTopico.LastOrDefault()!.Observacao == request.Observacao)
                return Response("The status of the Topico has not changed.");

            revisaoTopico.ForEach(rt =>
            {
                rt.Delete();
                _revisaoTopicoRepository.Update(rt);
            });

            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError(nameof(PostRevisaoTopicoHandler), "Error creating revision for Topico:", e);
            return Response();
        }
    }
}