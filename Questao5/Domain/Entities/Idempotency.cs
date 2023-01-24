using System.Net;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Questao5.Infrastructure.Database.Contracts;

namespace Questao5.Domain.Entities;

[Table("idempotencia")]
public class Idempotency
{
    public string Key { get; set; }
    public string Request { get; set; }
    public string Response { get; set; }

    public static async Task<string> GetCachedResponseOrNull(ActionExecutingContext context, IIdempotencyRepository idempotencyRepository)
    {
        string request = await GetStringFromBody(context.HttpContext.Request.Body); 
        return await idempotencyRepository.GetCachedResponseOrNull(request);
    }

    public static async Task SetCacheResponse(ResultExecutingContext context, IIdempotencyRepository idempotencyRepository, string headerKeyName)
    {
        string header = context.HttpContext.Request.Headers
            .First(x => x.Key.Equals(headerKeyName, StringComparison.OrdinalIgnoreCase)).Value;
        string request = await GetStringFromBody(context.HttpContext.Request.Body);
        string response = await GetStringFromBody(context.HttpContext.Request.Body);
        await idempotencyRepository.SetCacheResponse(header, request, response);
    }

    private static async Task<string> GetStringFromBody(Stream body)
    {
        using StreamReader streamReader = new (body);
        string bodyString = await streamReader.ReadToEndAsync(); 
        return bodyString;
    }
}