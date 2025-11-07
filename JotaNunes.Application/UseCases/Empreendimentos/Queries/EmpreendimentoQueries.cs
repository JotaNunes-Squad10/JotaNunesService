using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Empreendimentos.Queries;

public class EmpreendimentoQueries(
    IDomainService domainService,
    IEmpreendimentoBaseRepository repository,
    IUserRepository userRepository
) : BaseQueries<EmpreendimentoBase, EmpreendimentoBaseFullResponse, IEmpreendimentoBaseRepository>(domainService, repository), IEmpreendimentoQueries
{
    private readonly IEmpreendimentoBaseRepository _repository = repository;

    public new async Task<DefaultResponse> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

        var response = Map<List<EmpreendimentoBaseResponse>>(entities);

        response.ForEach(eb =>
        {
            var user = userRepository.GetByIdAsync(eb.UsuarioAlteracaoId).Result;
            eb.UsuarioAlteracao = user != null
                ? $"{user.FirstName ?? ""} {user.LastName ?? ""}"
                : "Não autorado";
        });

        return Response(response);
    }

    public async Task<DefaultResponse> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (IsNull(entity)) return Response();

        var response = Map<EmpreendimentoBaseFullResponse>(entity!);

        var user = userRepository.GetByIdAsync(response.UsuarioAlteracaoId).Result;
        response.UsuarioAlteracao = user != null
            ? $"{user.FirstName ?? ""} {user.LastName ?? ""}"
            : "Não autorado";

        return Response(response);
    }

    public async Task<DefaultResponse> GetByVersionAsync(Guid id, int version)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (IsNull(entity)) return Response();

        var empreendimentos = entity!.Empreendimentos.Where(x => x.Versao == version).ToList();

        if (ListIsNullOrEmpty(empreendimentos)) return Response();

        entity.Empreendimentos = empreendimentos;

        var response = Map<EmpreendimentoBaseFullResponse>(entity);

        var user = userRepository.GetByIdAsync(response.UsuarioAlteracaoId).Result;
        response.UsuarioAlteracao = user != null
            ? $"{user.FirstName ?? ""} {user.LastName ?? ""}"
            : "Não autorado";

        return Response(response);
    }
}