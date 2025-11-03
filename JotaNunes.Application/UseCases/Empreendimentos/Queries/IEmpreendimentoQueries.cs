using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Empreendimentos.Queries;

public interface IEmpreendimentoQueries : IBaseQueries
{
    Task<DefaultResponse> GetByIdAsync(Guid id);
    Task<DefaultResponse> GetByVersionAsync(Guid id, int version);
}
