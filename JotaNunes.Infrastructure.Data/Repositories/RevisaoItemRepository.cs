using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class RevisaoItemRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<RevisaoItem>(applicationContext, domainService), IRevisaoItemRepository
{
    public async Task<RevisaoItem?> GetLastByItemIdAsync(long id)
        => await GetTracking
            .Include(x => x.AmbienteItem)
            .OrderBy(x => x.DataHoraInclusao)
            .LastOrDefaultAsync(x => x.ItemId == id);
}