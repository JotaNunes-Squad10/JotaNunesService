using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class RevisaoMaterialRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<RevisaoMaterial>(applicationContext, domainService), IRevisaoMaterialRepository
{
    public async Task<RevisaoMaterial?> GetLastByMaterialIdAsync(long id)
        => await GetTracking
            .Include(x => x.TopicoMaterial)
            .OrderBy(x => x.DataHoraInclusao)
            .LastOrDefaultAsync(x => x.MaterialId == id);
}