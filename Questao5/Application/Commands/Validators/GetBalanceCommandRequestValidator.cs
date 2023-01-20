using FluentValidation;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Contracts;

namespace Questao5.Application.Commands.Validators;

public class GetBalanceCommandRequestValidator : AbstractValidator<GetBalanceCommandRequest>
{
    public GetBalanceCommandRequestValidator(IAccountRepository accountRepository)
    {
        RuleFor(request => request.AccountId)
            .Cascade(CascadeMode.Stop)
            .SetAsyncValidator(new IsAccountRegisteredValidator<GetBalanceCommandRequest>(accountRepository))
            .SetAsyncValidator(new IsAccountActiveValidator<GetBalanceCommandRequest>(accountRepository));
    }
}