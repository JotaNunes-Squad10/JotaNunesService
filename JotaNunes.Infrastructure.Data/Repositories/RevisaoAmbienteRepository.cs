using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class RevisaoAmbienteRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<RevisaoAmbiente>(applicationContext, domainService), IRevisaoAmbienteRepository
{
    public async Task<List<RevisaoAmbiente>> GetByAmbienteIdAsync(long id)
        => await GetTracking
            .Include(x => x.TopicoAmbiente)
            .Where(x => x.AmbienteId == id)
            .ToListAsync();
}