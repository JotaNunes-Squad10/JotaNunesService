using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Empreendimentos.Queries;

public class EmpreendimentoQueries(IDomainService domainService, IEmpreendimentoBaseRepository repository)
    : BaseQueries<EmpreendimentoBase, EmpreendimentoResultResponse, IEmpreendimentoBaseRepository>(domainService, repository), IEmpreendimentoQueries
{
    private readonly IEmpreendimentoBaseRepository _repository = repository;

    public async Task<DefaultResponse> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (IsNull(entity)) return Response();

        var response = Map<EmpreendimentoResultResponse>(entity!);

        return Response(response);
    }

    public async Task<DefaultResponse> GetByVersionAsync(Guid id, int version)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (IsNull(entity)) return Response();

        var empreendimentos = entity!.Empreendimentos.Where(x => x.Versao == version).ToList();

        if (ListIsNullOrEmpty(empreendimentos)) return Response();

        entity.Empreendimentos = empreendimentos;

        var response = Map<EmpreendimentoResultResponse>(entity);

        return Response(response);
    }
}