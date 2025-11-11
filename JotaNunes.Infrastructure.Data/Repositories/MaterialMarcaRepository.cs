using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class MaterialMarcaRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<MaterialMarca>(applicationContext, domainService), IMaterialMarcaRepository
{
    public override async Task<List<MaterialMarca>> GetAllAsync()
        => await GetTracking
            .Include(x => x.Material)
            .Include(x => x.Marca)
            .ToListAsync();

    public override async Task<MaterialMarca?> GetByIdAsync(long id)
        => await GetTracking
            .Include(x => x.Material)
            .Include(x => x.Marca)
            .FirstOrDefaultAsync(x => x.Id == id);
}