using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Contracts;

public interface ITransactionRepository
{
    Task<string> Save(Transaction transaction);
    Task<IEnumerable<Transaction>> GetAll();
    Task<bool> IsAccountRegistered(string id);
    Task<bool> IsAccountActive(string id);
}