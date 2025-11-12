using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IRevisaoAmbienteRepository : IBaseRepository<RevisaoAmbiente>
{
    Task<RevisaoAmbiente?> GetLastByAmbienteIdAsync(long id);
}