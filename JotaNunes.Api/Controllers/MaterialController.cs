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
    [HttpPost("CreateMaterial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMaterialAsync([FromBody] CreateMaterialRequest request)
        => CustomResponse(await Send(request));

    [HttpDelete("DeleteMaterial/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMaterialAsync([FromRoute] long id)
        => CustomResponse(await Send(new DeleteMaterialRequest { Id = id }));

    [HttpGet("GetAllMateriais")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllMateriaisAsync()
        => CustomResponse(await materialQueries.GetAllAsync());

    [HttpGet("GetMaterialById/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMaterialByIdAsync([FromRoute] long id)
        => CustomResponse(await materialQueries.GetByIdAsync(id));

    [HttpPut("UpdateMaterial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMaterialAsync([FromBody] UpdateMaterialRequest request)
        => CustomResponse(await Send(request));

    [HttpPost("SetMaterialComentario")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostRevisaoMaterialAsync([FromBody] PostRevisaoMaterialRequest request)
        => CustomResponse(await Send(request));

    [HttpDelete("ClearMaterialComentario/{materialId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ClearRevisaoMaterialAsync([FromRoute] long materialId)
        => CustomResponse(await Send(new ClearRevisaoMaterialRequest { MaterialId = materialId }));
}