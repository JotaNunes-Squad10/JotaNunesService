using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Handlers;

public class UpdateItemStatusHandler(
    IDomainService domainService,
    IAmbienteItemRepository ambienteItemRepository,
    IRevisaoItemRepository revisaoItemRepository
) : BaseHandler<RevisaoItem, UpdateItemStatusRequest, ItemStatusResponse, IRevisaoItemRepository>(domainService, revisaoItemRepository),
    IRequestHandler<UpdateItemStatusRequest, DefaultResponse>
{
    private readonly IRevisaoItemRepository _revisaoItemRepository = revisaoItemRepository;

    public async Task<DefaultResponse> Handle(UpdateItemStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var ambienteItem = await ambienteItemRepository.GetByIdAsync(request.ItemId);

            if (IsNull(ambienteItem))
            {
                AddError("UpdateAmbienteItemHandler", "Item not found");
                return Response();
            }

            var revisaoItem = await _revisaoItemRepository.GetLastByItemIdAsync(request.ItemId);

            if (revisaoItem != null || revisaoItem?.StatusId == request.StatusId)
                return Response("Item status not updated");

            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateAmbienteItemHandler", "Error updating item status:", e);
            return Response();
        }
    }
}