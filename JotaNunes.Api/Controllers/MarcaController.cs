using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Marca.Commands.Requests;
using JotaNunes.Application.UseCases.Marca.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class MarcaController(
    IMediator mediator,
    IMarcaQueries queries
) : BaseController(mediator)
{
    [HttpPost("CreateMarca")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMarcaAsync([FromBody] CreateMarcaRequest request)
        => CustomResponse(await Send(request));

    [HttpPut("UpdateMarca")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMarcaAsync([FromBody] UpdateMarcaRequest request)
        => CustomResponse(await Send(request));

    [HttpGet("GetAllMarcas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllMarcasAsync()
        => CustomResponse(await queries.GetAllAsync());

    [HttpGet("GetMarcaById/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMarcaByIdAsync(long id)
        => CustomResponse(await queries.GetByIdAsync(id));

    [HttpDelete("DeleteMarca/{id:long}")]
    [ProducesResponseType(StatusCodes.Status20OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMarcaAsync(long id)
        => CustomResponse(await Send(new DeleteMarcaRequest(id)));
}