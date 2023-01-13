using System.ComponentModel;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Internal;
using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

[Table("movimento")]
public class Transaction
{
    [Key]
    [Description("idmovimento")]
    public string Id { get; set; }
    [Description("idcontacorrente")]
    public string AccountId { get; set; }
    [Description("valor")]
    public double Amount { get; set; }
    [Description("tipomovimento")]
    public TransactionType TransactionType { get; set; }
    [Description("datamovimentacao")]
    public DateTimeOffset Date { get; set; }
}