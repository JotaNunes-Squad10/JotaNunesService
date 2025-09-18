using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;

namespace JotaNunes.Application.UseCases.Item.Queries;

public class ItemQueries(IDomainService domainService, IItemRepository repository)
    : BaseQueries<Domain.Models.Item, ItemResponse, IItemRepository>(domainService, repository), IItemQueries
{
    
}