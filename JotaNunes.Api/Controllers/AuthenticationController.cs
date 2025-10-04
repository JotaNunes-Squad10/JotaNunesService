using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Authentication.Commands.Requests;
using JotaNunes.Application.UseCases.Authentication.Queries;
using JotaNunes.Domain.Attributes;
using JotaNunes.Domain.Models.Keycloak;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class AuthenticationController(
    IMediator mediator,
    IAuthenticationQueries authenticationQueries
) : BaseController(mediator)
{
    [HttpPost("Authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        => CustomResponse(await Send(request));

    [AuthorizeGroup(Group.Administrador)]
    [HttpDelete("DeleteUser/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
        => CustomResponse(await Send(new DeleteUserRequest { Id = id }));

    [AuthorizeGroup(Group.Administrador)]
    [HttpPost("CreateUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        => CustomResponse(await Send(request));

    [AuthorizeGroup(Group.Administrador)]
    [HttpGet("GetAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllUsersAsync()
        => CustomResponse(await authenticationQueries.GetAllAsync());

    [HttpGet("GetUserById/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        => CustomResponse(await authenticationQueries.GetByIdAsync(id));

    [HttpGet("GetUserByUsername/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByUsername([FromRoute] string username)
        => CustomResponse(await authenticationQueries.GetByUsernameAsync(username));

    [AuthorizeGroup(Group.Administrador)]
    [HttpPatch("ResetPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        => CustomResponse(await Send(request));

    [HttpPatch("UpdatePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordRequest request)
        => CustomResponse(await Send(request));

    [AuthorizeGroup(Group.Administrador)]
    [HttpPatch("UpdateUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
        => CustomResponse(await Send(request));
}
