using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = Questao5.Application.Exceptions.ValidationException;

namespace Questao5.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            ValidationContext<TRequest> context = new(request);

            IEnumerable<ValidationResult> validationResults = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(result => result.Errors.Any())
                .SelectMany(result => result.Errors);

            if (failures.Any())
                throw new ValidationException(failures);
        }

        return await next();
    }
}