using FluentValidation;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.Contracts;

namespace Questao5.Application.Commands.Validators;

public class TransactionCommandRequestValidator : AbstractValidator<TransactionCommandRequest>
{
    public TransactionCommandRequestValidator(ITransactionRepository transactionRepository)
    {
        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .MustAsync((id, _) => transactionRepository.IsAccountRegistered(id))
            .WithMessage("TYPE: INVALID_ACCOUNT")
            .MustAsync((id, _) => transactionRepository.IsAccountActive(id))
            .WithMessage("TYPE: INACTIVE_ACCOUNT");

        RuleFor(request => request.Amount)
            .GreaterThan(0)
            .WithMessage("TYPE: INVALID_VALUE");

        RuleFor(request => request.TransactionType)
            .IsInEnum()
            .WithMessage("TYPE: INVALID_TYPE");
    }
}