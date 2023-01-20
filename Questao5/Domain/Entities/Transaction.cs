using System.ComponentModel;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Internal;
using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

[Table("movimento")]
public class Transaction
{
    [Key]
    public string Id { get; set; }
    public Guid AccountId { get; set; }
    public double Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTimeOffset Date { get; set; }
}