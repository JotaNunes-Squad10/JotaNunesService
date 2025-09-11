using JotaNunes.Application.UseCases.Base;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Application.UseCases.Item.Responses;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using JotaNunes.Application.UseCases.Topico.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Handlers;

public class CreateTopicoHandler(
    IDomainService domainService,
    ITopicoRepository repository
) : BaseHandler<Domain.Models.Topico, CreateTopicoRequest, TopicoResponse, ITopicoRepository>(domainService, repository),
    IRequestHandler<CreateTopicoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateTopicoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("CreateTopicoHandler", "Error creating topico:", e);
            return Response();
        }
    }
}