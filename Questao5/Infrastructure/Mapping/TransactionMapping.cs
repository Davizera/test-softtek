using Dapper.FluentMap.Mapping;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Mapping;

public class TransactionMapping : EntityMap<Transaction>
{
    public TransactionMapping()
    {
        Map(transaction => transaction.Id)
            .ToColumn("idmovimento");

        Map(transaction => transaction.AccountId)
            .ToColumn("idcontacorrente");

        Map(transaction => transaction.TransactionType)
            .ToColumn("tipomovimento");
        
        Map(transaction => transaction.Date)
            .ToColumn("datamovimento");
        
        Map(transaction => transaction.Amount)
            .ToColumn("valor");
    }
}