using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.Marca.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
namespace JotaNunes.Application.UseCases.Marca.Queries;

public class MarcaQueries(IDomainService domainService, IMarcaRepository repository)
    : BaseQueries<Domain.Models.Marca, MarcaResponse, IMarcaRepository>(domainService, repository), IMarcaQueries
{
    
}
