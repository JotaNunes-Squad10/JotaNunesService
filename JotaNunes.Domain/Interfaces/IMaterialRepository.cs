using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IMaterialRepository : IBaseRepository<Material>
{
    Task<List<Material>> GetAllWithMarcasAsync();
    Task<Material?> GetByIdWithMarcasAsync(long id);
}