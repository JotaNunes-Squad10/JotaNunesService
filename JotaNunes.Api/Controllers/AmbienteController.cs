using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class AmbienteController(
    IMediator mediator
) : BaseController(mediator)
{
    [HttpPost("CreateAmbiente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAmbienteAsync([FromBody] CreateAmbienteRequest request)
        => CustomResponse(await Send(request));
    
    [HttpPatch("UpdateAmbiente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAmbienteAsync([FromBody] UpdateAmbienteRequest request)
        => CustomResponse(await Send(request));
}