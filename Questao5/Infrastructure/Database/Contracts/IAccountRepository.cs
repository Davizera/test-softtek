namespace Questao5.Infrastructure.Database.Contracts;

public interface IAccountRepository
{
    Task<bool> IsAccountRegistered(Guid id);
    Task<bool> IsAccountActive(Guid id);
}