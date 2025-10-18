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
    public override async Task<List<Empreendimento>> GetAllAsync()
        => await GetTracking
            .Include(x => x.EmpreendimentoStatus)
            .Include(x => x.EmpreendimentoPadrao)
            .Include(x => x.EmpreendimentoTopicos)
                .ThenInclude(x => x.Topico)
            .Include(x => x.EmpreendimentoAmbientes)
                .ThenInclude(x => x.Ambiente)
            .Include(x => x.EmpreendimentoItens)
                .ThenInclude(x => x.Item)
            .Include(x => x.EmpreendimentoMaterials)
                .ThenInclude(x => x.Material)
                    .ThenInclude(x => x.Marca)
            .OrderBy(x => x.Nome)
            .ToListAsync();

    public override async Task<Empreendimento?> GetByIdAsync(long id)
        => await GetTracking
            .Include(x => x.EmpreendimentoStatus)
            .Include(x => x.EmpreendimentoPadrao)
            .Include(x => x.EmpreendimentoTopicos)
                .ThenInclude(x => x.Topico)
            .Include(x => x.EmpreendimentoAmbientes)
                .ThenInclude(x => x.Ambiente)
            .Include(x => x.EmpreendimentoItens)
                .ThenInclude(x => x.Item)
            .Include(x => x.EmpreendimentoMaterials)
                .ThenInclude(x => x.Material)
                    .ThenInclude(x => x.Marca)
            .FirstOrDefaultAsync(x => x.Id == id);
}
