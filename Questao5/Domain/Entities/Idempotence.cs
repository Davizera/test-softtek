using Dapper.Contrib.Extensions;

namespace Questao5.Domain.Entities;

[Table("idempotencia")]
public class Idempotence
{
    public string Key { get; set; }
    public string Request { get; set; }
    public string Response { get; set; }
}