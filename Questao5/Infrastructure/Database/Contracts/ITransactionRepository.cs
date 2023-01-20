using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Contracts;

public interface ITransactionRepository
{
    Task<Guid> Save(Transaction transaction);
}