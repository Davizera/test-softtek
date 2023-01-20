using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Database.Contracts;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly SqliteConnection _connection;
    public BalanceRepository(DatabaseConfig databaseConfig)
    {
        _connection = new SqliteConnection(databaseConfig.Name);
    }
    
    public async Task<(double, string)> GetBalanceAndAccountName(Guid id)
    {
        var debitAmountTask = GetDebitTransactions(id);
        var creditAmountTask = GetCreditTransactions(id);
        var accountHolderNameTask = GetAccountOwnerName(id);

        await Task.WhenAll(debitAmountTask, creditAmountTask, accountHolderNameTask);

        double balance = creditAmountTask.Result - debitAmountTask.Result;
        
        return (balance, accountHolderNameTask.Result);
    }

    private async Task<string> GetAccountOwnerName(Guid id)
    {
        string accountOwnerName;
        await using (_connection)
        {
            const string sql = @"select nome from contacorrente where idcontacorrente like @id";
            accountOwnerName = await _connection.QueryFirstAsync<string>(sql, new {id});
        }

        return accountOwnerName;
    }

    private async Task<double> GetDebitTransactions(Guid id)
    {
        double debitAmount;
        await using (_connection)
        {
            const string sql = @"select coalesce(sum(valor), 0) from movimento where idcontacorrente like @id and tipomovimento like 'D'";
            debitAmount = await _connection.QueryFirstOrDefaultAsync<double>(sql, new {id});
        }

        return debitAmount;
    }
    
    private async Task<double> GetCreditTransactions(Guid id)
    {
        double creditAmount;
        await using (_connection)
        {
            const string sql = @"select coalesce(sum(valor), 0) from movimento where idcontacorrente like @id and tipomovimento like 'C'";
            creditAmount = await _connection.QueryFirstOrDefaultAsync<double>(sql, new {id});
        }

        return creditAmount;
    }

}