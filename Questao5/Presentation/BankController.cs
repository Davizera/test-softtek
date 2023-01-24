using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Filters;
using Questao5.Infrastructure.Database.Contracts;
using Questao5.Infrastructure.Database.Repositories;

namespace Questao5.Presentation;

[ApiController]
[Route("api/[controller]")]
public class BankController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BankController(IMediator mediator) => (_mediator) = (mediator);

    [
        ProducesResponseType(typeof(TransactionCommandResponse), (int) HttpStatusCode.OK),
        ProducesResponseType(typeof(BadRequestResult), (int) HttpStatusCode.BadRequest),
        ProducesResponseType((int) HttpStatusCode.InternalServerError), HttpPut, 
        Idempotent()
    ]
    public async Task<IActionResult> Transaction(TransactionCommandRequest request)
    {
        TransactionCommandResponse result = await _mediator.Send(request);
        return Ok(result);
    }

    [ProducesResponseType(typeof(GetBalanceCommandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet("GetBalance/{id}")]
    public async Task<IActionResult> GetBalance(Guid id)
    {
        GetBalanceCommandRequest request = new()
        {
            AccountId = id
        };
        GetBalanceCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}