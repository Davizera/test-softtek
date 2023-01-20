using MediatR;
using Microsoft.Extensions.Internal;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Infrastructure.Database.Contracts;

namespace Questao5.Application.Handlers;

public class GetBalanceCommandHandler : IRequestHandler<GetBalanceCommandRequest, GetBalanceCommandResponse>
{
    private readonly IBalanceRepository _repository;
    private readonly ISystemClock _clock;

    public GetBalanceCommandHandler(IBalanceRepository repository, ISystemClock clock)
    {
        _repository = repository;
        _clock = clock;
    }
    
    public async Task<GetBalanceCommandResponse> Handle(GetBalanceCommandRequest request, CancellationToken cancellationToken)
    {
        (double balance, string accountOwnerName) = await _repository.GetBalanceAndAccountName(request.AccountId);

        return new GetBalanceCommandResponse
        {
            AccountId = request.AccountId,
            AccountOwnerName = accountOwnerName,
            Balance = balance,
            QueriedAt = _clock.UtcNow
        };
    }
}