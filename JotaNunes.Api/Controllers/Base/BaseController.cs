using Asp.Versioning;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers.Base;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/[controller]")]
public class BaseController(IMediator mediator) : ControllerBase
{
    protected Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        => mediator.Send(request);
    
    protected ActionResult CustomResponse(DefaultResponse response)
    {
        var errors = response.ValidationResult.Errors.Select(x => x.ErrorMessage);
        
        if (response.ValidationResult.Errors.Count == 0)
            return Ok(response);

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]> { { "Messages", errors.ToArray() } }));
    }
}