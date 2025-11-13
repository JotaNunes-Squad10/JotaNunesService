using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IRevisaoTopicoRepository : IBaseRepository<RevisaoTopico>
{
    Task<RevisaoTopico?> GetLastByTopicoIdAsync(long id);
}