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

    [HttpPut("UpdateEmpreendimento")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmpreendimentoAsync([FromBody] UpdateEmpreendimentoRequest request)
        => CustomResponse(await Send(request));

    [HttpGet("GetAllEmpreendimentos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllEmpreendimentosAsync()
        => CustomResponse(await queries.GetAllAsync());

    [HttpGet("GetEmpreendimentoById/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmpreendimentoByIdAsync(long id)
        => CustomResponse(await queries.GetByIdAsync(id));

    [HttpDelete("DeleteEmpreendimento/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteEmpreendimentoAsync(long id)
        => CustomResponse(await Send(new DeleteEmpreendimentoRequest(id)));
}
