using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Handlers;

public class ClearRevisaoItemHandler(
    IDomainService domainService,
    IAmbienteItemRepository ambienteItemRepository,
    IRevisaoItemRepository revisaoItemRepository
) : BaseHandler<RevisaoItem, ClearRevisaoItemRequest, RevisaoItemResponse, IRevisaoItemRepository>(domainService, revisaoItemRepository),
    IRequestHandler<ClearRevisaoItemRequest, DefaultResponse>
{
    private readonly IRevisaoItemRepository _revisaoItemRepository = revisaoItemRepository;

    public async Task<DefaultResponse> Handle(ClearRevisaoItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var ambienteItem = await ambienteItemRepository.GetByIdAsync(request.ItemId);

            if (IsNull(ambienteItem))
            {
                AddError(nameof(PostRevisaoItemHandler), "Item not found.");
                return Response();
            }

            var revisoesItem = await _revisaoItemRepository.GetByItemIdAsync(request.ItemId);

            if (revisoesItem is { Count: > 0 })
            {
                revisoesItem.ForEach(ri =>
                {
                    ri.Delete();
                    _revisaoItemRepository.Update(ri);
                });
            }

            await CommitAsync();
            return Response("Item revisions cleared.");
        }
        catch (Exception e)
        {
            AddError(nameof(ClearRevisaoItemHandler), "Error deleting Item revisions:", e);
            return Response();
        }
    }
}