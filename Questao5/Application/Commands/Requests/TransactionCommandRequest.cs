using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests;


public class TransactionCommandRequest : IRequest<TransactionCommandResponse>
{
    public Guid Id { get; set; }
    public double Amount { get; set; }
    public string TransactionType { get; set; }
}