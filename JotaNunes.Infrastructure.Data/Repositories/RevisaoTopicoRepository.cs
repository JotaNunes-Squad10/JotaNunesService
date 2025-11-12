using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class RevisaoTopicoRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<RevisaoTopico>(applicationContext, domainService), IRevisaoTopicoRepository
{
    public async Task<RevisaoTopico?> GetLastByTopicoIdAsync(long id)
        => await GetTracking
            .Include(x => x.EmpreendimentoTopico)
            .OrderBy(x => x.DataHoraInclusao)
            .LastOrDefaultAsync(x => x.TopicoId == id);
}