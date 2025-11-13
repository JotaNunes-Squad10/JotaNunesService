using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.MarcaMateriais.Queries;

public interface IMarcaMaterialQueries : IBaseQueries
{
    Task<DefaultResponse> GetAllGroupByMarcaIdAsync(long marcaId);
    Task<DefaultResponse> GetAllGroupByMaterialIdAsync(long materialId);
    Task<DefaultResponse> GetAllMarcasByMaterialIdAsync(long materialId);
    Task<DefaultResponse> GetAllMateriaisByMarcaIdAsync(long marcaId);
}