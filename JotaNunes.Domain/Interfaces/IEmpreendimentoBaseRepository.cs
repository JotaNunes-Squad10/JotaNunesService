using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IEmpreendimentoBaseRepository : IBaseRepository<EmpreendimentoBase>
{
    Task<List<EmpreendimentoBase>> GetAllFullAsync();
    Task<EmpreendimentoBase?> GetByIdAsync(Guid id);
    Task<EmpreendimentoBase?> GetByVersionAsync(Guid id, int versao);
    Task<int> GetLastVersionAsync(Guid id);
}