using Microsoft.Extensions.Internal;

namespace Questao5.Application.Commands.Responses;

public class GetBalanceCommandResponse
{
    public Guid AccountId { get; set; }
    public string AccountOwnerName { get; set; }
    public DateTimeOffset QueriedAt { get; set; }
    public double Balance { get; set; }
}