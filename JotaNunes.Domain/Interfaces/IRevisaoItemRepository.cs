using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IRevisaoItemRepository : IBaseRepository<RevisaoItem>
{
    Task<List<RevisaoItem>> GetByItemIdAsync(long id);
}