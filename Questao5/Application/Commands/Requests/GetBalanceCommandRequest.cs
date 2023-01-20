using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests;

public class GetBalanceCommandRequest : IRequest<GetBalanceCommandResponse>
{
    public Guid AccountId { get; set; }
}