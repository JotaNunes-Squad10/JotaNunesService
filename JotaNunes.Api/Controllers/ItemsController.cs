using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class ItemsController(
    IMediator mediator,
    IItemQueries itemQueries
) : BaseController(mediator)
{
    [HttpPost("CreateItem")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemRequest request)
        => CustomResponse(await Send(request));

    [HttpDelete("DeleteItem/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteItemAsync([FromRoute] long id)
        => CustomResponse(await Send(new DeleteItemRequest { Id = id }));

    [HttpGet("GetAllItems")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllItemsAsync()
        => CustomResponse(await itemQueries.GetAllAsync());

    [HttpGet("GetItemById/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemByIdAsync([FromRoute] long id)
        => CustomResponse(await itemQueries.GetByIdAsync(id));

    [HttpPatch("UpdateItem")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateItemAsync([FromBody] UpdateItemRequest request)
        => CustomResponse(await Send(request));

    [HttpPost("SetItemComentario")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
        => CustomResponse(await Send(request));
}