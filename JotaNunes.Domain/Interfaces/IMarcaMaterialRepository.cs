using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IMarcaMaterialRepository : IBaseRepository<MarcaMaterial>
{
    Task<List<MarcaMaterial>> GetByMarcaIdAsync(long marcaId);
    Task<List<MarcaMaterial>> GetByMaterialIdAsync(long materialId);
}