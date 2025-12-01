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
    public new async Task<DefaultResponse> GetAllAsync()
    {
        var entities = await Repository.GetAllAsync();

        var response = Map<List<EmpreendimentoBaseResponse>>(entities);

        var users = await userRepository.GetAllAsync();
        response.ForEach(void (eb) =>
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
            var version = await Repository.GetLastVersionAsync(id);

            if (IsNull(version)) return Response();

            var entity = await Repository.GetByVersionAsync(id, version);

            if (IsNull(entity)) return Response();

            var response = Map<EmpreendimentoBaseFullResponse>(entity!);

            var users = await userRepository.GetAllAsync();
            var user = users.FirstOrDefault(x => x.Id == response.UsuarioAlteracaoId);

            response.UsuarioAlteracao = user != null
                ? $"{user.FirstName ?? ""} {user.LastName ?? ""}"
                : "Não autorado";

            if (response.Empreendimentos is { Count: > 0 })
            {
                response.Empreendimentos.ForEach(void (e) =>
                {
                    user = users.FirstOrDefault(x => x.Id == e.UsuarioAlteracaoId);
                    e.UsuarioAlteracao = user != null
                        ? $"{user.FirstName ?? ""} {user.LastName ?? ""}"
                        : "Não autorado";
                });
            }

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
        var entity = await Repository.GetByVersionAsync(id, version);

        if (IsNull(entity)) return Response();

        var empreendimentos = entity!.Empreendimentos.Where(x => x.Versao == version).ToList();

        if (ListIsNullOrEmpty(empreendimentos)) return Response();

        entity.Empreendimentos = empreendimentos;

        var response = Map<EmpreendimentoBaseFullResponse>(entity);

        var users = await userRepository.GetAllAsync();
        var user = users.FirstOrDefault(x => x.Id == response.UsuarioAlteracaoId);

        response.UsuarioAlteracao = user != null
            ? $"{user.FirstName ?? ""} {user.LastName ?? ""}"
            : "Não autorado";

        if (response.Empreendimentos is { Count: > 0 })
        {
            response.Empreendimentos.ForEach(void (e) =>
            {
                user = users.FirstOrDefault(x => x.Id == e.UsuarioAlteracaoId);
                e.UsuarioAlteracao = user != null
                    ? $"{user.FirstName ?? ""} {user.LastName ?? ""}"
                    : "Não autorado";
            });
        }

        return Response(response);
    }
}