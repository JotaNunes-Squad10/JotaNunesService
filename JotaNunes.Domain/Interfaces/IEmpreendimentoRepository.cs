using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IEmpreendimentoRepository : IBaseRepository<Empreendimento>
{
    Task AppendVersionToRelationsAsync(Guid empreendimentoBaseId, int nextVersion);
}