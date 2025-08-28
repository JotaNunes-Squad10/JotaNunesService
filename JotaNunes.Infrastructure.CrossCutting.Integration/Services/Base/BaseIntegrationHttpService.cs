using JotaNunes.Domain.Services;
using JotaNunes.Domain.Services.Base;
using JotaNunes.Infrastructure.CrossCutting.Commons.Helpers;
using JotaNunes.Infrastructure.CrossCutting.Integration.Requests.Keycloak;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Services.Base;

public class BaseIntegrationHttpService(
    HttpClient httpClient,
    IDomainService domainService
) : BaseHttpService(httpClient, domainService)
{
    protected static FormUrlEncodedContent  PrepareTokenRequest(TokenRequest request)
        => HttpContentHelper.ToFormUrlEncodedContent(request);
    
    protected static StringContent PrepareCreateUserRequest(CreateUserRequest request)
        => HttpContentHelper.ToJsonStringContent(request);
}