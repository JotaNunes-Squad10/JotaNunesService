using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using JotaNunes.Application.UseCases.Topico.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Handlers;

public class UpdateTopicoHandler(
    IDomainService domainService,
    ITopicoRepository repository
) : BaseHandler<Domain.Models.Public.Topico, UpdateTopicoRequest, TopicoResponse, ITopicoRepository>(domainService, repository),
    IRequestHandler<UpdateTopicoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateTopicoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await UpdateAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateTopicoHandler", "Error updating ambiente:", e);
            return Response();
        }
    }
}