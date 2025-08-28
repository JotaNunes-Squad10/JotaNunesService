using System.Net.Http.Headers;
using JotaNunes.Infrastructure.CrossCutting.Commons.Helpers;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;
using Newtonsoft.Json;

namespace JotaNunes.Domain.Services.Base;

public class BaseHttpService(HttpClient httpClient, IDomainService domainService)
{
    protected ExternalServices ExternalServices => domainService.AppProvider.ExternalServices;
    
    protected void SetBaseAddress(string baseUrlService)
        => httpClient.BaseAddress = new Uri(baseUrlService);
    
    protected void SetAuthorization(string scheme, string parameter)
        => httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
    
    protected void SetHeader(string name, string value)
        => httpClient.DefaultRequestHeaders.Add(name, value);
    
    protected void AddError(string property, string message)
        => domainService.Notifications.AddError(property, message);

    protected async Task<HttpResponseMessage> GetAsync(string route)
        => await httpClient.GetAsync(route);

    protected async Task<T?> GetAsync<T>(string route)
    {
        var response = await httpClient.GetAsync(route);
        var stringResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(stringResponse, HttpContentHelper.SnakeCaseSettings);
    }
    
    protected async Task<HttpResponseMessage> PostAsync(string route, HttpContent request)
        => await httpClient.PostAsync(route, request);
    
    protected async Task<T> PostAsync<T>(string route, HttpContent request)
    {
        var response = await httpClient.PostAsync(route, request);
        var stringResponse = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
            AddError(nameof(T), $"{response.StatusCode}: {stringResponse}");
        
        var result = JsonConvert.DeserializeObject<T>(stringResponse, HttpContentHelper.SnakeCaseSettings);
        return result ?? throw new Exception(response.ReasonPhrase);
    }
}