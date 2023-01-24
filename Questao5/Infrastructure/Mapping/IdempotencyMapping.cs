using Dapper.FluentMap.Mapping;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Mapping;

public class IdempotencyMapping : EntityMap<Idempotency>
{
    public IdempotencyMapping()
    {
        Map(idempotency => idempotency.Key)
            .ToColumn("chave_idempotencia");
        
        Map(idempotency => idempotency.Request)
            .ToColumn("requisicao");
        
        Map(idempotency => idempotency.Response)
            .ToColumn("resultado");
    }
}