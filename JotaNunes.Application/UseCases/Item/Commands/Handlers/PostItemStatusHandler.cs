using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Handlers;

public class PostItemStatusHandler(
    IDomainService domainService,
    IAmbienteItemRepository ambienteItemRepository,
    IRevisaoItemRepository revisaoItemRepository
) : BaseHandler<RevisaoItem, PostItemStatusRequest, ItemStatusResponse, IRevisaoItemRepository>(domainService, revisaoItemRepository),
    IRequestHandler<PostItemStatusRequest, DefaultResponse>
{
    private readonly IRevisaoItemRepository _revisaoItemRepository = revisaoItemRepository;

    public async Task<DefaultResponse> Handle(PostItemStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var ambienteItem = await ambienteItemRepository.GetByIdAsync(request.ItemId);

            if (IsNull(ambienteItem))
            {
                AddError(nameof(PostItemStatusHandler), "Item not found");
                return Response();
            }

            var revisoesItem = await _revisaoItemRepository.GetByItemIdAsync(request.ItemId);

            if (revisoesItem is { Count: > 0 }
                && revisoesItem.LastOrDefault()!.StatusId == request.StatusId
                && revisoesItem.LastOrDefault()!.Observacao == request.Observacao)
                return Response("Item status not updated");

            revisoesItem.ForEach(ri =>
            {
                ri.Delete();
                _revisaoItemRepository.Update(ri);
            });

            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError(nameof(PostItemStatusHandler), "Error updating item status:", e);
            return Response();
        }
    }
}