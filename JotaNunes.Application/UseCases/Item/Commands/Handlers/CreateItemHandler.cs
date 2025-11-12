using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Item.Commands.Handlers;

public class CreateItemHandler(
    IDomainService domainService,
    IItemRepository repository
) : BaseHandler<Domain.Models.Public.Item, CreateItemRequest, ItemResponse, IItemRepository>(domainService, repository),
    IRequestHandler<CreateItemRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("CreateItemHandler", "Error creating item:", e);
            return Response();
        }
    }
}