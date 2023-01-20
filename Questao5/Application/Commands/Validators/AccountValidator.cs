using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using Questao5.Infrastructure.Database.Contracts;

namespace Questao5.Application.Commands.Validators;

public class IsAccountRegisteredValidator<T> : AsyncPropertyValidator<T, Guid> where T : class
{
    private readonly IAccountRepository _accountRepository;

    public IsAccountRegisteredValidator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid id, CancellationToken cancellation)
    {
        bool isAccountRegistered = await _accountRepository.IsAccountRegistered(id);

        return isAccountRegistered;
    }

    public override string Name => nameof(IsAccountRegisteredValidator<T>);

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "TYPE: INVALID_ACCOUNT";
    }
}
public class IsAccountActiveValidator<T> : AsyncPropertyValidator<T, Guid> where T : class
{
    private readonly IAccountRepository _accountRepository;

    public IsAccountActiveValidator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid id, CancellationToken cancellation)
    {
        bool isAccountActive = await _accountRepository.IsAccountActive(id);

        return isAccountActive;
    }

    public override string Name => nameof(IsAccountActiveValidator<T>);

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "TYPE: INACTIVE_ACCOUNT";
    }
}