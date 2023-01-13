using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Contracts;
using Questao5.Infrastructure.Sqlite;
using Transaction = Questao5.Domain.Entities.Transaction;

namespace Questao5.Infrastructure.Database.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private SqliteConnection _connection { get; }

    public TransactionRepository(DatabaseConfig databaseConfig)
    {
        _connection = new SqliteConnection(databaseConfig.Name);
    }

    public async Task<string> Save(Transaction transaction)
    {
        var id = Guid.NewGuid();
        await using (_connection)
        {
            var sql = @"insert into movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                values (@Id, @AccountId, @Date, @TransactionType, @Amount);
                select last_insert_rowid();";

            await _connection.ExecuteScalarAsync(sql,
                new
                {
                    Id = id,
                    transaction.AccountId,
                    transaction.Date,
                    TransactionType = transaction.TransactionType.ToString(),
                    transaction.Amount
                });
        }

        return id.ToString();
    }

    public Task<IEnumerable<Transaction>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsAccountRegistered(string id)
    {
        bool isAccountRegistered;

        await using (_connection)
        {
            const string sql = @"select idcontacorrente,
                    numero,
                    nome,
                    ativo
                from contacorrente where idcontacorrente = @id";
            var account = await _connection.QueryFirstOrDefaultAsync<Account>(sql, new {id});
            isAccountRegistered = account is not null;
        }

        return isAccountRegistered;
    }

    public async Task<bool> IsAccountActive(string id)
    {
        bool isActive;

        await using (_connection)
        {
            const string sql = "select * from contacorrente where idcontacorrente = @id";
            var account = await _connection.QuerySingleOrDefaultAsync<Account>(sql, new {id});
            isActive = account?.IsActive ?? false;
        }

        return isActive;
    }
}