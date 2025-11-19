using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IRevisaoTopicoRepository : IBaseRepository<RevisaoTopico>
{
    Task<List<RevisaoTopico>> GetByTopicoIdAsync(long id);
}