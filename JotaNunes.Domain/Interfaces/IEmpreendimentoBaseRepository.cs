using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IEmpreendimentoBaseRepository : IBaseRepository<EmpreendimentoBase>
{
    public Task<EmpreendimentoBase?> GetByIdAsync(Guid id);
    public Task<EmpreendimentoBase?> GetByVersionAsync(Guid id, int versao);
    public Task<int> GetLastVersionAsync(Guid id);
}