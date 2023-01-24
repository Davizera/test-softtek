using Dapper;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Contracts;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;

public class IdempotencyRepository : IIdempotencyRepository
{
    private readonly SqliteConnection _connection;

    public IdempotencyRepository(DatabaseConfig databaseConfig)
    {
        _connection = new SqliteConnection(databaseConfig.Name);
    }

    public async Task<string> GetCachedResponseOrNull(string request)
    {
        Idempotency result;
        await using (_connection)
        {
            const string sql = @"select * from idempotencia where requisicao like @request";
            result = await _connection.QueryFirstOrDefaultAsync<Idempotency>(sql, new {request});
        }

        return result?.Response;
    }

    public async Task SetCacheResponse(string id, string request, string response)
    {
        await using (_connection)
        {
            const string sql = @"insert into idempotencia (chave_idempotencia, requisicao, resultado) 
                values (@Id, @Request, @Response)";
            await _connection.ExecuteAsync(sql, new {id, request, response});
        }
    }
}