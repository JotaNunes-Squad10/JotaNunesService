using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;

namespace JotaNunes.Application.UseCases.Ambiente.Queries;

public class AmbienteQueries(IDomainService domainService, IAmbienteRepository repository)
    : BaseQueries<Domain.Models.Ambiente, AmbienteResponse, IAmbienteRepository>(domainService, repository), IAmbienteQueries
{
    
}