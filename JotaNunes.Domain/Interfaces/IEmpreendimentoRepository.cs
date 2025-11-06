using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Domain.Interfaces;

public interface IEmpreendimentoRepository : IBaseRepository<Empreendimento>
{
    Task AppendVersionToTopicosAsync(long[] etIds, int nextVersion);
    Task AppendVersionToAmbientesAsync(long[] taIds, int nextVersion);
    Task AppendVersionToMateriaissAsync(long[] tmIds, int nextVersion);
    Task AppendVersionToItensAsync(long[] aiIds, int nextVersion);
}