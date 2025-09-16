using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Base.Queries;

public interface IBaseQueries
{
    Task<DefaultResponse> GetAllAsync();
    Task<DefaultResponse> GetByIdAsync(long id);
    void AddError(Exception e);
}