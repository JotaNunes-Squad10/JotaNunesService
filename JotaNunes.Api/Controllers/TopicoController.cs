using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using JotaNunes.Application.UseCases.Topico.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class TopicoController(
    IMediator mediator,
    ITopicoQueries topicoQueries
) : BaseController(mediator)
{
    [HttpPost("CreateTopico")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTopicoAsync([FromBody] CreateTopicoRequest request)
        => CustomResponse(await Send(request));

    [HttpDelete("DeleteTopico/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTopicoAsync([FromRoute] long id)
        => CustomResponse(await Send(new DeleteTopicoRequest { Id = id }));

    [HttpGet("GetAllTopicos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllTopicosAsync()
        => CustomResponse(await topicoQueries.GetAllAsync());

    [HttpGet("GetTopicoById/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTopicoByIdAsync([FromRoute] long id)
        => CustomResponse(await topicoQueries.GetByIdAsync(id));

    [HttpPatch("UpdateTopico")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTopicoAsync([FromBody] UpdateTopicoRequest request)
        => CustomResponse(await Send(request));
}