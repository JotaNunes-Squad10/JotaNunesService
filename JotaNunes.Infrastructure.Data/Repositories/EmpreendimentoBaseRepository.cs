using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class EmpreendimentoBaseRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<EmpreendimentoBase>(applicationContext, domainService), IEmpreendimentoBaseRepository
{
    public override async Task<List<EmpreendimentoBase>> GetAllAsync()
        => await GetTracking
            .Include(eb => eb.LogsStatus)
            .Include(eb => eb.Empreendimentos)
            .Include(eb => eb.EmpreendimentoStatus)
            .Include(eb => eb.Empreendimentos)
                .ThenInclude(e => e.EmpreendimentoPadrao)
            .Include(eb => eb.EmpreendimentoTopicos)
                .ThenInclude(et => et.Topico)
            .Include(eb => eb.EmpreendimentoTopicos)
                .ThenInclude(et => et.TopicoAmbientes)
                        .ThenInclude(ta => ta.Ambiente)
                            .ThenInclude(a => a.Topico)
            .Include(eb => eb.EmpreendimentoTopicos)
                .ThenInclude(et => et.TopicoAmbientes)
                    .ThenInclude(ta => ta.AmbienteItens)
                            .ThenInclude(ai => ai.Item)
            .Include(eb => eb.EmpreendimentoTopicos)
                .ThenInclude(et => et.TopicoMateriais)
                    .ThenInclude(tm => tm.MaterialMarca)
                        .ThenInclude(m => m.Marca)
            .ToListAsync();

    public async Task<EmpreendimentoBase?> GetByIdAsync(Guid id)
        => await GetTracking
            .Include(eb => eb.LogsStatus)
            .Include(eb => eb.Empreendimentos)
            .Include(eb => eb.EmpreendimentoStatus)
            .Include(eb => eb.Empreendimentos)
                .ThenInclude(e => e.EmpreendimentoPadrao)
            .Include(eb => eb.EmpreendimentoTopicos)
                .ThenInclude(et => et.Topico)
            .Include(eb => eb.EmpreendimentoTopicos)
                .ThenInclude(et => et.TopicoAmbientes)
                        .ThenInclude(ta => ta.Ambiente)
                            .ThenInclude(a => a.Topico)
            .Include(eb => eb.EmpreendimentoTopicos)
                .ThenInclude(et => et.TopicoAmbientes)
                    .ThenInclude(ta => ta.AmbienteItens)
                            .ThenInclude(ai => ai.Item)
            .Include(eb => eb.EmpreendimentoTopicos)
                .ThenInclude(et => et.TopicoMateriais)
                    .ThenInclude(tm => tm.MaterialMarca)
                        .ThenInclude(m => m.Marca)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<EmpreendimentoBase?> GetByVersionAsync(Guid id, int version)
        => await Get
            .Include(eb => eb.LogsStatus)
            .Include(eb => eb.Empreendimentos)
            .Include(eb => eb.EmpreendimentoStatus)
            .Include(eb => eb.Empreendimentos)
                .ThenInclude(e => e.EmpreendimentoPadrao)
            .Include(eb => eb.EmpreendimentoTopicos.Where(et => et.Versoes.Contains(version)))
                .ThenInclude(et => et.Topico)
            .Include(eb => eb.EmpreendimentoTopicos.Where(et => et.Versoes.Contains(version)))
                .ThenInclude(et => et.TopicoAmbientes)
                        .ThenInclude(ta => ta.Ambiente)
                            .ThenInclude(a => a.Topico)
            .Include(eb => eb.EmpreendimentoTopicos.Where(et => et.Versoes.Contains(version)))
                .ThenInclude(et => et.TopicoAmbientes.Where(ta => ta.Versoes.Contains(version)))
                    .ThenInclude(ta => ta.AmbienteItens)
                            .ThenInclude(ai => ai.Item)
            .Include(eb => eb.EmpreendimentoTopicos.Where(et => et.Versoes.Contains(version)))
                .ThenInclude(et => et.TopicoMateriais.Where(tm => tm.Versoes.Contains(version)))
                    .ThenInclude(tm => tm.MaterialMarca)
                        .ThenInclude(m => m.Marca)
            .FirstOrDefaultAsync(eb => eb.Id == id);

    public async Task<int> GetLastVersionAsync(Guid id)
        => (await Get.Include(eb => eb.Empreendimentos)
            .FirstOrDefaultAsync(eb => eb.Id == id))!
            .Empreendimentos.MaxBy(x => x.Versao)!.Versao;
}