using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Contracts;
using Microsoft.Extensions.Internal;

namespace Questao5.Application.Handlers;

public class TransactionCommandHandler : IRequestHandler<TransactionCommandRequest, TransactionCommandResponse>
{
    private readonly ITransactionRepository _repository;
    private readonly ISystemClock _clock;

    public TransactionCommandHandler(ITransactionRepository repository, ISystemClock clock)
    {
        _repository = repository;
        _clock = clock;
    }
    
    public async Task<TransactionCommandResponse> Handle(TransactionCommandRequest request, CancellationToken cancellationToken)
    {
        Transaction transaction = new()
        {
            AccountId = request.Id,
            TransactionType = request.TransactionType,
            Amount = request.Amount,
            Date = _clock.UtcNow,
        };
        var response = await _repository.Save(transaction);

        return new TransactionCommandResponse {Id = response};
    }
}