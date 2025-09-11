using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class TopicoController(
    IMediator mediator
) : BaseController(mediator)
{
    [HttpPost("CreateTopico")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTopicoAsync([FromBody] CreateTopicoRequest request)
        => CustomResponse(await Send(request));
}
