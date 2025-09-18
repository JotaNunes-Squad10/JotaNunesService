using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.Empreendimento.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
namespace JotaNunes.Application.UseCases.Empreendimento.Queries;
public class EmpreendimentoQueries(IDomainService domainService, IEmpreendimentoRepository repository)
    : BaseQueries<Domain.Models.Empreendimento, EmpreendimentoResponse, IEmpreendimentoRepository>(domainService, repository), IEmpreendimentoQueries
{
    
}