using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Empreendimento.Queries;

public interface IEmpreendimentoQueries : IBaseQueries
{
    Task<DefaultResponse> GetByIdAsync(Guid id);
    Task<DefaultResponse> GetByIdVersionAsync(Guid id, long version);
}