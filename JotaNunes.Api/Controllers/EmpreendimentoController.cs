using JotaNunes.Api.Controllers.Base;
using JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotaNunes.Api.Controllers;

public class EmpreendimentoController(
    IMediator mediator
) : BaseController(mediator)
{
    [HttpPost("CreateEmpreendimento")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmpreendimentoAsync([FromBody] CreateEmpreendimentoRequest request)
        => CustomResponse(await Send(request));
}
