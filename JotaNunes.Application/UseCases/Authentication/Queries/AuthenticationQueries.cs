using JotaNunes.Application.UseCases.Authentication.Responses;
using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.Authentication.Queries;

public class AuthenticationQueries(IDomainService domainService, IUserRepository repository)
    : BaseQueries<Domain.Models.User, UserResponse, IUserRepository>(domainService, repository), IAuthenticationQueries
{
    public new async Task<DefaultResponse> GetAllAsync()
    {
        var list = (await Repository.GetAllAsync())
            .Where(u => u.FirstName != null)
            .ToList();

        if (ListIsNullOrEmpty(list)) return Response();

        return Response(Map(list));
    }
    
    public virtual async Task<DefaultResponse> GetByIdAsync(Guid id)
    {
        var entity = await Repository.GetByIdAsync(id);

        if (IsNull(entity)) return Response();

        return Response(Map(entity!));
    }
}