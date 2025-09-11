using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Authentication.Commands.Requests;
using JotaNunes.Domain.Attributes;
using JotaNunes.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class AuthenticationController(
    IMediator mediator
) : BaseController(mediator)
{
    [HttpPost("Authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        => CustomResponse(await Send(request));
    
    [AuthorizeGroup(Group.Administrador)]
    [HttpPost("CreateUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        => CustomResponse(await Send(request));
}