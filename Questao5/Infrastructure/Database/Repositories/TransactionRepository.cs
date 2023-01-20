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

    public async Task<Guid> Save(Transaction transaction)
    {
        var id = Guid.NewGuid();
        await using (_connection)
        {
            const string sql = @"insert into movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                values (@Id, @AccountId, @Date, @TransactionType, @Amount);";

            await _connection.ExecuteScalarAsync(sql,
                new
                {
                    Id = id,
                    transaction.AccountId,
                    transaction.Date,
                    TransactionType = (char) transaction.TransactionType,
                    transaction.Amount
                });
        }

        return id;
    }
}