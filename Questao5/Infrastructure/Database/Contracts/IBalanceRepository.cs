namespace Questao5.Infrastructure.Database.Contracts;

public interface IBalanceRepository
{
    Task<(double, string)> GetBalanceAndAccountName(Guid id);
}