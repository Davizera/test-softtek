using FluentValidation;
using FluentValidation.Validators;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.Contracts;

namespace Questao5.Application.Commands.Validators;

public class TransactionCommandRequestValidator : AbstractValidator<TransactionCommandRequest>
{
    public TransactionCommandRequestValidator(IAccountRepository accountRepository)
    {
        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .SetAsyncValidator(new IsAccountRegisteredValidator<TransactionCommandRequest>(accountRepository))
            .SetAsyncValidator(new IsAccountActiveValidator<TransactionCommandRequest>(accountRepository));

        RuleFor(request => request.Amount)
            .GreaterThan(0)
            .WithMessage("TYPE: INVALID_VALUE");

        RuleFor(request => request.TransactionType)
            .IsEnumName(typeof(TransactionType))
            .WithMessage("TYPE: INVALID_TYPE");
    }
}