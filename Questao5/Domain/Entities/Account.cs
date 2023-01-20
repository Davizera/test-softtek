using Dapper.Contrib.Extensions;
using FluentValidation;
using Questao5.Application.Commands.Requests;
using Questao5.Infrastructure.Database.Contracts;

namespace Questao5.Domain.Entities;

[Table("contacorrente")]
public class Account
{
    [Key] public Guid Id { get; set; }

    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public bool IsActive { get; set; }
}