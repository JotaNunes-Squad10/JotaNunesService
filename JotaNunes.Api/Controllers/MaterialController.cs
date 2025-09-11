using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class MaterialController(
    IMediator mediator
) : BaseController(mediator)
{
    [HttpPost("CreateMaterial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMaterialAsync([FromBody] CreateMaterialRequest request)
        => CustomResponse(await Send(request));
}