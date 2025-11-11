using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.MaterialMarcas.Commands.Requests;
using JotaNunes.Application.UseCases.MaterialMarcas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class MaterialMarcaController(
    IMediator mediator,
    IMaterialMarcaQueries materialMarcaQueries
) : BaseController(mediator)
{
    [HttpPost("CreateMaterialMarca")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMaterialMarcaAsync([FromBody] CreateMaterialMarcaRequest request)
        => CustomResponse(await Send(request));

    [HttpDelete("DeleteMaterialMarca/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMaterialMarcaAsync([FromRoute] long id)
        => CustomResponse(await Send(new DeleteMaterialMarcaRequest { Id = id }));

    [HttpGet("GetAllMaterialMarcas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllMaterialMarcasAsync()
        => CustomResponse(await materialMarcaQueries.GetAllAsync());

    [HttpGet("GetMaterialMarcaById/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMaterialMarcaByIdAsync([FromRoute] long id)
        => CustomResponse(await materialMarcaQueries.GetByIdAsync(id));

    [HttpGet("GetAllMarcasByMaterialId/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMarcasByMaterialIdAsync([FromRoute] long id)
        => CustomResponse(await materialMarcaQueries.GetAllMarcasByMaterialIdAsync(id));

    [HttpGet("GetAllMateriaisByMarcaId/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMateriaisByMarcaIdAsync([FromRoute] long id)
        => CustomResponse(await materialMarcaQueries.GetAllMateriaisByMarcaIdAsync(id));

    [HttpPatch("UpdateMaterialMarca")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMaterialMarcaAsync([FromBody] UpdateMaterialMarcaRequest request)
        => CustomResponse(await Send(request));
}