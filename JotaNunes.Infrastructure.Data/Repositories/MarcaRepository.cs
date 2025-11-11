using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class MarcaRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<Marca>(applicationContext, domainService), IMarcaRepository
{
    public async Task<Marca?> GetByIdWithMateriaisAsync(long id)
        => await GetTracking
            .Include(x => x.MaterialMarcas)
                .ThenInclude(x => x.Material)
            .FirstOrDefaultAsync(x => x.Id == id);
}