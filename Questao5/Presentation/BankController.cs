using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Presentation;

[ApiController]
[Route("api/[controller]")]
public class BankController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BankController(IMediator mediator) => _mediator = mediator;

    [HttpPut]
    public async Task<IActionResult> Transaction([FromBody]TransactionCommandRequest transaction)
    {
        TransactionCommandResponse result = await _mediator.Send(transaction);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBalance(object account) => Ok(new {account});
}