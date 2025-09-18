using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;

namespace JotaNunes.Application.UseCases.Material.Queries;

public class MaterialQueries(IDomainService domainService, IMaterialRepository repository)
    : BaseQueries<Domain.Models.Material, MaterialResponse, IMaterialRepository>(domainService, repository), IMaterialQueries
{
    
}