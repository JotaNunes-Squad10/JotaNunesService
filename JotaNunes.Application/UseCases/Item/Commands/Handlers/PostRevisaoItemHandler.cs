using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Handlers;

public class PostRevisaoItemHandler(
    IDomainService domainService,
    IAmbienteItemRepository ambienteItemRepository,
    IRevisaoItemRepository revisaoItemRepository
) : BaseHandler<RevisaoItem, PostRevisaoItemRequest, RevisaoItemResponse, IRevisaoItemRepository>(domainService, revisaoItemRepository),
    IRequestHandler<PostRevisaoItemRequest, DefaultResponse>
{
    private readonly IRevisaoItemRepository _revisaoItemRepository = revisaoItemRepository;

    public async Task<DefaultResponse> Handle(PostRevisaoItemRequest request, CancellationToken cancellationToken)
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

            if (revisoesItem is { Count: > 0 }
                && revisoesItem.LastOrDefault()!.StatusId == request.StatusId
                && revisoesItem.LastOrDefault()!.Observacao == request.Observacao)
                return Response("The status of the Item has not changed.");

            revisoesItem.ForEach(ri =>
            {
                ri.Delete();
                _revisaoItemRepository.Update(ri);
            });

            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError(nameof(PostRevisaoItemHandler), "Error creating revision for Item:", e);
            return Response();
        }
    }
}