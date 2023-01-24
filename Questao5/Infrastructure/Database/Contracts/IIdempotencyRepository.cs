namespace Questao5.Infrastructure.Database.Contracts;

public interface IIdempotencyRepository
{
    Task<string> GetCachedResponseOrNull(string request);

    Task SetCacheResponse(string id, string request, string response);
}