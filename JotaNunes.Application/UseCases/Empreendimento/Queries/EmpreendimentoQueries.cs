using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.Empreendimento.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Empreendimento.Queries;

public class EmpreendimentoQueries(IDomainService domainService, IEmpreendimentoRepository repository)
    : BaseQueries<Domain.Models.Public.Empreendimento, EmpreendimentoResponse, IEmpreendimentoRepository>(domainService, repository), IEmpreendimentoQueries
{
    public override async Task<DefaultResponse> GetByIdAsync(long id)
    {
        var entity = await Repository.GetByIdAsync(id);

        if (IsNull(entity)) return Response();

        var response = Map<EmpreendimentoFullResponse>(entity!);

        return Response(response);
    }
}