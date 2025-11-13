using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IRevisaoMaterialRepository : IBaseRepository<RevisaoMaterial>
{
    Task<RevisaoMaterial?> GetLastByMaterialIdAsync(long id);
}