using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;
using JotaNunes.Application.UseCases.MarcaMateriais.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class MarcaMaterialController(
    IMediator mediator,
    IMarcaMaterialQueries materialMarcaQueries
) : BaseController(mediator)
{
    [HttpDelete("DeleteMarcaMaterial/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMarcaMaterialAsync([FromRoute] long id)
        => CustomResponse(await Send(new DeleteMarcaMaterialRequest { Id = id }));

    [HttpGet("GetAllGroupByMarcaId/{marcaId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllGroupByMarcaId([FromRoute] long marcaId)
        => CustomResponse(await materialMarcaQueries.GetAllGroupByMarcaIdAsync(marcaId));

    [HttpGet("GetAllGroupByMaterialId/{materialId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllGroupByMaterialId([FromRoute] long materialId)
        => CustomResponse(await materialMarcaQueries.GetAllGroupByMaterialIdAsync(materialId));

    [HttpGet("GetAllMarcaMateriais")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllMarcaMateriaisAsync()
        => CustomResponse(await materialMarcaQueries.GetAllAsync());

    [HttpGet("GetMarcaMaterialById/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMarcaMaterialByIdAsync([FromRoute] long id)
        => CustomResponse(await materialMarcaQueries.GetByIdAsync(id));

    [HttpGet("GetAllMarcasByMaterialId/{materialId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMarcasByMaterialIdAsync([FromRoute] long materialId)
        => CustomResponse(await materialMarcaQueries.GetAllMarcasByMaterialIdAsync(materialId));

    [HttpGet("GetAllMateriaisByMarcaId/{marcaId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMateriaisByMarcaIdAsync([FromRoute] long marcaId)
        => CustomResponse(await materialMarcaQueries.GetAllMateriaisByMarcaIdAsync(marcaId));

    [HttpPatch("UpdateMarcaMaterial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMarcaMaterialAsync([FromBody] UpdateMarcaMaterialRequest request)
        => CustomResponse(await Send(request));
}