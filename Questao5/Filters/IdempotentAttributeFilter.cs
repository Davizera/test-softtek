using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Contracts;

namespace Questao5.Filters;

public class IdempotentAttributeFilter : IAsyncActionFilter, IAsyncResultFilter
{
    private readonly IIdempotencyRepository _idempotencyRepository;
    private const string _headerKeyName = "IdempotentKey";

    public IdempotentAttributeFilter(IIdempotencyRepository idempotencyRepository) =>
        (_idempotencyRepository) = (idempotencyRepository);

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //Verify idempotent request headers
        //Short circuit if IdempotentKey not present
        if (!context.HttpContext.Request.Headers.ContainsKey(_headerKeyName))
        {
            ShortCircuitMissingIdempotentHeaderKey(context);
            return;
        }

        //Check if the request has already been processed 
        //If processed return cached response

        string response = await Idempotency.GetCachedResponseOrNull(context, _idempotencyRepository);

        if (response is not null)
            context.Result = new ContentResult()
            {
                Content = response,
                ContentType = "application/json",
                StatusCode = (int?) HttpStatusCode.OK
            };

        await next();
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        //Otherwise, continue to run the action
        //After action has finished cache the response even if it returns an error
        await Idempotency.SetCacheResponse(context, _idempotencyRepository, _headerKeyName);
    }

    private void ShortCircuitMissingIdempotentHeaderKey(ActionExecutingContext context)
    {
        ProblemDetails problemDetails = new()
        {
            Title = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = "Missing Idempotent Header Key",
            Status = (int?) HttpStatusCode.BadRequest
        };

        context.Result = new BadRequestObjectResult(problemDetails);
    }
}