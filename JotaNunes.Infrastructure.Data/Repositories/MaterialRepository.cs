using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class MaterialRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<Material>(applicationContext, domainService), IMaterialRepository
{
    public async Task<List<Material>> GetAllWithMarcasAsync()
        => await GetTracking
            .Include(x => x.MarcaMateriais)
            .ThenInclude(x => x.Marca)
            .ToListAsync();

    public async Task<Material?> GetByIdWithMarcasAsync(long id)
        => await GetTracking
            .Include(x => x.MarcaMateriais)
            .ThenInclude(x => x.Marca)
            .FirstOrDefaultAsync(x => x.Id == id);
}