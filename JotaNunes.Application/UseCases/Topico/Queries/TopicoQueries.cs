using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.Topico.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;

namespace JotaNunes.Application.UseCases.Topico.Queries;

public class TopicoQueries(IDomainService domainService, ITopicoRepository repository)
    : BaseQueries<Domain.Models.Topico, TopicoResponse, ITopicoRepository>(domainService, repository), ITopicoQueries
{
    
}