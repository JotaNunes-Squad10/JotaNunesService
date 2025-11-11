using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.MaterialMarcas.Queries;

public interface IMaterialMarcaQueries : IBaseQueries
{
    Task<DefaultResponse> GetAllMarcasByMaterialIdAsync(long id);
    Task<DefaultResponse> GetAllMateriaisByMarcaIdAsync(long id);
}