using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Marca.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class MarcaController(
    IMediator mediator
) : BaseController(mediator)
{
    [HttpPost("CreateMarca")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMarcaAsync([FromBody] CreateMarcaRequest request)
        => CustomResponse(await Send(request));
}