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
    
    [HttpPatch("UpdateTopico")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAmbienteAsync([FromBody] UpdateTopicoRequest request)
        => CustomResponse(await Send(request));
}
