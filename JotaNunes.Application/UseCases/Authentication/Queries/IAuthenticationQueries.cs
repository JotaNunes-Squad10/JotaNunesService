using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Authentication.Queries;

public interface IAuthenticationQueries : IBaseQueries
{
    new Task<DefaultResponse> GetAllAsync();
    Task<DefaultResponse> GetByIdAsync(Guid id);
    Task<DefaultResponse> GetByUsernameAsync(string username);
    Task<DefaultResponse> GetRequiredActionsByUsernameAsync(string username);
}