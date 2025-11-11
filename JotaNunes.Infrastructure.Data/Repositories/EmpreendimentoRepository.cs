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

    public async Task AppendVersionToTopicosAsync(long[] etIds, int nextVersion)
    {
        await _ctx.Database.ExecuteSqlInterpolatedAsync($@"
            UPDATE public.rl_empreendimento_topico
            SET versoes = array_append(coalesce(versoes, '{{}}'), {nextVersion})
            WHERE id = ANY({etIds})
              AND NOT ({nextVersion} = ANY(coalesce(versoes, '{{}}')));
        ");
    }

    public async Task AppendVersionToAmbientesAsync(long[] taIds, int nextVersion)
    {
        await _ctx.Database.ExecuteSqlInterpolatedAsync($@"
            UPDATE public.rl_topico_ambiente
            SET versoes = array_append(coalesce(versoes, '{{}}'), {nextVersion})
            WHERE id = ANY({taIds})
            AND NOT ({nextVersion} = ANY(coalesce(versoes, '{{}}')));
        ");
    }

    public async Task AppendVersionToMateriaissAsync(long[] tmIds, int nextVersion)
    {
        await _ctx.Database.ExecuteSqlInterpolatedAsync($@"
            UPDATE public.rl_topico_material
            SET versoes = array_append(coalesce(versoes, '{{}}'), {nextVersion})
            WHERE id = ANY({tmIds})
            AND NOT ({nextVersion} = ANY(coalesce(versoes, '{{}}')));
        ");
    }

    public async Task AppendVersionToItensAsync(long[] aiIds, int nextVersion)
    {
        await _ctx.Database.ExecuteSqlInterpolatedAsync($@"
            UPDATE public.rl_ambiente_item
            SET versoes = array_append(coalesce(versoes, '{{}}'), {nextVersion})
            WHERE id = ANY({aiIds})
            AND NOT ({nextVersion} = ANY(coalesce(versoes, '{{}}')));
        ");
    }
}