using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Contracts;
using Microsoft.Extensions.Internal;
using Questao5.Domain.Enumerators;

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
        var transactionType = Enum.Parse<TransactionType>(request.TransactionType);
        
        Transaction transaction = new()
        {
            AccountId = request.Id,
            TransactionType = transactionType,
            Amount = request.Amount,
            Date = _clock.UtcNow,
        };
        
        Guid id = await _repository.Save(transaction);

        return new TransactionCommandResponse {Id = id};
    }
}