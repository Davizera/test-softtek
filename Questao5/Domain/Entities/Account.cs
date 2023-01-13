using Dapper.Contrib.Extensions;

namespace Questao5.Domain.Entities;

[Table("contacorrente")]
public class Account
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public bool IsActive { get; set; }
}