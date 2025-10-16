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
    public override async Task<List<Material>> GetAllAsync()
        => await GetTracking
            .Include(x => x.Marca)
            .ToListAsync();

    public override async Task<Material?> GetByIdAsync(long id)
        => await GetTracking
            .Include(x => x.Marca)
            .FirstOrDefaultAsync(x => x.Id == id);
}