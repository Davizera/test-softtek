using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Questao5.Domain.Enumerators;

public enum TransactionType
{
    Credit = 'C',
    Debit = 'D'
}