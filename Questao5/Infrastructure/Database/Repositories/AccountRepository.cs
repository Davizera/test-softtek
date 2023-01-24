using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Contracts;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;

public class AccountRepository : IAccountRepository
{
    private SqliteConnection _connection { get; }
    public AccountRepository(DatabaseConfig databaseConfig)
    {
        _connection = new SqliteConnection(databaseConfig.Name);
    }
    public async Task<bool> IsAccountRegistered(Guid id)
    {
        bool isAccountRegistered;

        await using (_connection)
        {
            const string sql = @"select idcontacorrente,
                    numero,
                    nome,
                    ativo
                from contacorrente where idcontacorrente like @id";
            var account = await _connection.QueryFirstOrDefaultAsync<Account>(sql, new {id});
            isAccountRegistered = account is not null;
        }

        return isAccountRegistered;
    }

    public async Task<bool> IsAccountActive(Guid id)
    {
        bool isActive;

        await using (_connection)
        {
            const string sql = "select * from contacorrente where idcontacorrente like @id";
            var account = await _connection.QuerySingleOrDefaultAsync<Account>(sql, new {id});
            isActive = account?.IsActive ?? false;
        }

        return isActive;
    }
}