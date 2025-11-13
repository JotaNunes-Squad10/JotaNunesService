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

        var users = await userRepository.GetAllAsync();
        response.ForEach(async void (eb) =>
        {
            var user = users.FirstOrDefault(x => x.Id == eb.UsuarioAlteracaoId);
            eb.UsuarioAlteracao = user != null
                ? $"{user.FirstName ?? ""} {user.LastName ?? ""}"
                : "Não autorado";
        });

        return Response(response);
    }

    public async Task<DefaultResponse> GetByIdAsync(Guid id)
    {
        try
        {
            var version = await _repository.GetLastVersionAsync(id);

            if (IsNull(version)) return Response();

            var entity = await _repository.GetByVersionAsync(id, version);

            if (IsNull(entity)) return Response();

            var response = Map<EmpreendimentoBaseFullResponse>(entity!);

            var user = await userRepository.GetByIdAsync(response.UsuarioAlteracaoId);
            response.UsuarioAlteracao = user != null
                ? $"{user.FirstName ?? ""} {user.LastName ?? ""}"
                : "Não autorado";

            return Response(response);
        }
        catch (Exception e)
        {
            AddError(e);
            return Response();
        }
    }

    public async Task<DefaultResponse> GetByVersionAsync(Guid id, int version)
    {
        var entity = await _repository.GetByVersionAsync(id, version);

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