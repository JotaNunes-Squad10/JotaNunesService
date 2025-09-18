using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class MaterialController(
    IMediator mediator,
    IMaterialQueries materialQueries
) : BaseController(mediator)
{
    [HttpDelete("DeleteMaterial/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMaterialAsync([FromRoute] long id)
        => CustomResponse(await Send(id));
    
    [HttpPost("CreateMaterial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMaterialAsync([FromBody] CreateMaterialRequest request)
        => CustomResponse(await Send(request));
    
    [HttpGet("GetAllMateriais")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllMateriaisAsync()
        => CustomResponse(await materialQueries.GetAllAsync());
    
    [HttpGet("GetMateriaisById/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMateriaisById([FromRoute] long id)
        => CustomResponse(await materialQueries.GetByIdAsync(id));
    
    [HttpPatch("UpdateMaterial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMaterialAsync([FromBody] UpdateMaterialRequest request)
        => CustomResponse(await Send(request));
}