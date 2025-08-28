using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JotaNunes.Infrastructure.CrossCutting.Commons.Helpers;

public static class HttpContentHelper
{
    private static readonly SnakeCaseNamingStrategy SnakeCase = new();
    
    public static readonly JsonSerializerSettings CamelCaseSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        }
    };
    
    public static readonly JsonSerializerSettings SnakeCaseSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        }
    };
    
    public static StringContent ToJsonStringContent(object obj)
    {
        var json = JsonConvert.SerializeObject(obj, SnakeCaseSettings);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
    
    public static FormUrlEncodedContent ToFormUrlEncodedContent(object obj)
    {
        var dict = obj.GetType()
            .GetProperties()
            .ToDictionary(
                p => SnakeCase.GetPropertyName(p.Name, false),
                p => p.GetValue(obj)?.ToString() ?? string.Empty
            );

        return new FormUrlEncodedContent(dict);
    }
}