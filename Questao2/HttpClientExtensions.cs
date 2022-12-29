using Microsoft.AspNetCore.WebUtilities;

namespace Questao2;

public static class HttpClientExtensions
{
    public static async Task<string> GetWithQueryString(this HttpClient client, string endpoint ,IDictionary<string, string> query)
    {
        return await client.GetStringAsync(QueryHelpers.AddQueryString(endpoint, query));
    }
}