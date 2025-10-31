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
            .Include(x => x.EmpreendimentoStatus)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoPadrao)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.Topico)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.TopicoAmbientes)
                            .ThenInclude(x => x.Ambiente)
                                .ThenInclude(x => x.Topico)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.TopicoAmbientes)
                        .ThenInclude(x => x.AmbienteItens)
                                .ThenInclude(x => x.Item)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.TopicoMateriais)
                        .ThenInclude(x => x.Material)
                            .ThenInclude(x => x.Marca)
            .ToListAsync();

    public async Task<EmpreendimentoBase?> GetByIdAsync(Guid id)
        => await GetTracking
            .Include(x => x.EmpreendimentoStatus)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoPadrao)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.Topico)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.TopicoAmbientes)
                            .ThenInclude(x => x.Ambiente)
                                .ThenInclude(x => x.Topico)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.TopicoAmbientes)
                        .ThenInclude(x => x.AmbienteItens)
                                .ThenInclude(x => x.Item)
            .Include(x => x.Empreendimentos)
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.TopicoMateriais)
                        .ThenInclude(x => x.Material)
                            .ThenInclude(x => x.Marca)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<EmpreendimentoBase?> GetByVersionAsync(Guid id, long version)
        => await Get
            .Include(x => x.EmpreendimentoStatus)
            .Include(x => x.Empreendimentos.Where(e => e.Versao == version))
                .ThenInclude(x => x.EmpreendimentoPadrao)
            .Include(x => x.Empreendimentos.Where(e => e.Versao == version))
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.Topico)
            .Include(x => x.Empreendimentos.Where(e => e.Versao == version))
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.TopicoAmbientes)
                            .ThenInclude(x => x.Ambiente)
                                .ThenInclude(x => x.Topico)
            .Include(x => x.Empreendimentos.Where(e => e.Versao == version))
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.TopicoAmbientes)
                        .ThenInclude(x => x.AmbienteItens)
                                .ThenInclude(x => x.Item)
            .Include(x => x.Empreendimentos.Where(e => e.Versao == version))
                .ThenInclude(x => x.EmpreendimentoTopicos)
                    .ThenInclude(x => x.TopicoMateriais)
                        .ThenInclude(x => x.Material)
                            .ThenInclude(x => x.Marca)
            .FirstOrDefaultAsync(x => x.Id == id);
}
