using Microsoft.AspNetCore.Mvc.Filters;
using Questao5.Infrastructure.Database.Contracts;

namespace Questao5.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class IdempotentAttribute : Attribute, IFilterFactory 
{
    public bool IsReusable { get; }


    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var idempotencyRepository = serviceProvider.GetService<IIdempotencyRepository>();
        return new IdempotentAttributeFilter(idempotencyRepository);
    }
}