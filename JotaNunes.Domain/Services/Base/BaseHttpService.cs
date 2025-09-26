using System.Net.Http.Headers;
using System.Text.Json;
using JotaNunes.Infrastructure.CrossCutting.Commons.Helpers;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace JotaNunes.Domain.Services.Base;

public abstract class BaseHttpService(HttpClient httpClient, IDomainService domainService)
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

    protected async void AddError(string property, HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var error = JsonSerializer.Deserialize<ErrorResponse>(content);
        AddError(property, $"{response.StatusCode}: {error?.Error ?? error?.ErrorDescription ?? error?.ErrorMessage}");
    }

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

    protected async Task<HttpResponseMessage> PutAsync(string route, HttpContent request)
        => await httpClient.PutAsync(route, request);

    protected async Task<T> PutAsync<T>(string route, HttpContent request)
    {
        var response = await httpClient.PutAsync(route, request);
        var stringResponse = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            AddError(nameof(T), $"{response.StatusCode}: {stringResponse}");

        var result = JsonConvert.DeserializeObject<T>(stringResponse, HttpContentHelper.SnakeCaseSettings);
        return result ?? throw new Exception(response.ReasonPhrase);
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string route)
        => await httpClient.DeleteAsync(route);
}