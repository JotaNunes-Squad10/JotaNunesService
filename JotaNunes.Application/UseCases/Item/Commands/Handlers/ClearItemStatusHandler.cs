using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Handlers;

public class ClearItemStatusHandler(
    IDomainService domainService,
    IAmbienteItemRepository ambienteItemRepository,
    IRevisaoItemRepository revisaoItemRepository
) : BaseHandler<RevisaoItem, ClearItemStatusRequest, RevisaoItemResponse, IRevisaoItemRepository>(domainService, revisaoItemRepository),
    IRequestHandler<ClearItemStatusRequest, DefaultResponse>
{
    private readonly IRevisaoItemRepository _revisaoItemRepository = revisaoItemRepository;

    public async Task<DefaultResponse> Handle(ClearItemStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var ambienteItem = await ambienteItemRepository.GetByIdAsync(request.ItemId);

            if (IsNull(ambienteItem))
            {
                AddError(nameof(PostRevisaoItemHandler), "Item not found");
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
            return Response("Item status cleared");
        }
        catch (Exception e)
        {
            AddError(nameof(ClearItemStatusHandler), "Error deleting item status:", e);
            return Response();
        }
    }
}