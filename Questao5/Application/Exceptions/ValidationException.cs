﻿using FluentValidation.Results;

namespace Questao5.Application.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException()
    {
        Errors = new Dictionary<string, string[]>();
    }
    
    public ValidationException(IEnumerable<ValidationFailure> failures) 
        : this()
    {
        Errors = failures
            .GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}