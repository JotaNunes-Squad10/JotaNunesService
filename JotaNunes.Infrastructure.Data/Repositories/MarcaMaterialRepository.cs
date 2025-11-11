using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class MarcaMaterialRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<MarcaMaterial>(applicationContext, domainService), IMarcaMaterialRepository
{
    public override async Task<List<MarcaMaterial>> GetAllAsync()
        => await GetTracking
            .Include(x => x.Marca)
            .Include(x => x.Material)
            .ToListAsync();

    public override async Task<MarcaMaterial?> GetByIdAsync(long id)
        => await GetTracking
            .Include(x => x.Marca)
            .Include(x => x.Material)
            .FirstOrDefaultAsync(x => x.Id == id);
}