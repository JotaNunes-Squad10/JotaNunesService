using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimento.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class EmpreendimentoController(
    IMediator mediator,
    IEmpreendimentoQueries queries
) : BaseController(mediator)
{
    [HttpPost("CreateEmpreendimento")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmpreendimentoAsync([FromBody] CreateEmpreendimentoRequest request)
        => CustomResponse(await Send(request));

    [HttpDelete("DeleteEmpreendimento/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteEmpreendimentoAsync([FromRoute] Guid id)
        => CustomResponse(await Send(new DeleteEmpreendimentoRequest { Id = id }));

    [HttpGet("GetAllEmpreendimentos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllEmpreendimentosAsync()
        => CustomResponse(await queries.GetAllAsync());

    [HttpPost("GenerateDocumentoEmpreendimento")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateDocumentoEmpreendimento([FromBody] GenerateDocumentoEmpreendimentoRequest request)
        => CustomResponse(await Send(request));

    [HttpGet("GetEmpreendimentoById/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmpreendimentoByIdAsync([FromRoute] Guid id)
        => CustomResponse(await queries.GetByIdAsync(id));

    [HttpGet("GetEmpreendimentoByVersion/{id:guid}/{versionNumber:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmpreendimentoByVersionAsync([FromRoute] Guid id, [FromRoute] int versionNumber)
        => CustomResponse(await queries.GetByVersionAsync(id, versionNumber));

    [HttpPatch("UpdateEmpreendimento")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmpreendimentoAsync([FromBody] UpdateEmpreendimentoStatusRequest statusRequest)
        => CustomResponse(await Send(statusRequest));
}