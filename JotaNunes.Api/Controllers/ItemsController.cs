using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class ItemsController(
    IMediator mediator
) : BaseController(mediator)
{
    [HttpPost("CreateItem")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemRequest request)
        => CustomResponse(await Send(request));
}