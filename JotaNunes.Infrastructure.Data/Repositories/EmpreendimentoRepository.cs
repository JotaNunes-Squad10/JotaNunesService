using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class EmpreendimentoRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<Empreendimento>(applicationContext, domainService), IEmpreendimentoRepository
{
    private readonly ApplicationContext _ctx = applicationContext;

    public async Task AppendVersionToRelationsAsync(Guid empreendimentoBaseId, int nextVersion)
    {
        await _ctx.Database.ExecuteSqlInterpolatedAsync($@"
            UPDATE public.rl_empreendimento_topico
            SET versoes = array_append(coalesce(versoes, '{{}}'), {nextVersion})
            WHERE empreendimento_fk = {empreendimentoBaseId}
              AND NOT ({nextVersion} = ANY(coalesce(versoes, '{{}}')));
        ");

        await _ctx.Database.ExecuteSqlInterpolatedAsync($@"
            UPDATE public.rl_topico_ambiente
            SET versoes = array_append(coalesce(versoes, '{{}}'), {nextVersion})
            WHERE topico_fk IN (
                SELECT id FROM public.rl_empreendimento_topico
                WHERE empreendimento_fk = {empreendimentoBaseId}
            )
            AND NOT ({nextVersion} = ANY(coalesce(versoes, '{{}}')));
        ");

        await _ctx.Database.ExecuteSqlInterpolatedAsync($@"
            UPDATE public.rl_topico_material
            SET versoes = array_append(coalesce(versoes, '{{}}'), {nextVersion})
            WHERE topico_fk IN (
                SELECT id FROM public.rl_empreendimento_topico
                WHERE empreendimento_fk = {empreendimentoBaseId}
            )
            AND NOT ({nextVersion} = ANY(coalesce(versoes, '{{}}')));
        ");

        await _ctx.Database.ExecuteSqlInterpolatedAsync($@"
            UPDATE public.rl_ambiente_item
            SET versoes = array_append(coalesce(versoes, '{{}}'), {nextVersion})
            WHERE ambiente_fk IN (
                SELECT id FROM public.rl_topico_ambiente
                WHERE topico_fk IN (
                    SELECT id FROM public.rl_empreendimento_topico
                    WHERE empreendimento_fk = {empreendimentoBaseId}
                )
            )
            AND NOT ({nextVersion} = ANY(coalesce(versoes, '{{}}')));
        ");
    }
}