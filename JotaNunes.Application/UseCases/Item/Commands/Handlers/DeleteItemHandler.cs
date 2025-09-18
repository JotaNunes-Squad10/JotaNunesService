using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Handlers;

public class DeleteItemHandler(
    IDomainService domainService,
    IItemRepository repository
) : BaseHandler<Domain.Models.Item, DeleteItemRequest, ItemResponse, IItemRepository>(domainService, repository),
    IRequestHandler<DeleteItemRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(DeleteItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await DeleteAsync(request.Id));
        }
        catch (Exception e)
        {
            AddError("DeleteItemHandler", "Error deleting item:", e);
            return Response();
        }
    }
}