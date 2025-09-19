using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Handlers;

public class UpdateItemHandler(
    IDomainService domainService,
    IItemRepository repository
) : BaseHandler<Domain.Models.Public.Item, UpdateItemRequest, ItemResponse, IItemRepository>(domainService, repository),
    IRequestHandler<UpdateItemRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await UpdateAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateItemHandler", "Error updating item:", e);
            return Response();
        }
    }
}