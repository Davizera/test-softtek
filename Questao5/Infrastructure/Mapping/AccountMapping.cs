using Dapper.FluentMap.Mapping;
using NSubstitute.ReturnsExtensions;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Mapping;

public class AccountMapping : EntityMap<Account>
{
    public AccountMapping()
    {
        Map(account => account.Id)
            .ToColumn("idcontacorrent");
        
        Map(account => account.AccountNumber)
            .ToColumn("numero");
        
        Map(account => account.IsActive)
            .ToColumn("ativo");
        
        Map(account => account.Name)
            .ToColumn("nome");
    }
}