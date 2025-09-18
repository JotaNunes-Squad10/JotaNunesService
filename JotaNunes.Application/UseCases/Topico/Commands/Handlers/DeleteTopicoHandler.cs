using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Topico.Commands.Requests;
using JotaNunes.Application.UseCases.Topico.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Handlers;

public class DeleteTopicoHandler(
    IDomainService domainService,
    ITopicoRepository repository
) : BaseHandler<Domain.Models.Topico, DeleteTopicoRequest, TopicoResponse, ITopicoRepository>(domainService, repository),
    IRequestHandler<DeleteTopicoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(DeleteTopicoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await DeleteAsync(request.Id));
        }
        catch (Exception e)
        {
            AddError("DeleteTopicoHandler", "Error deleting topico:", e);
            return Response();
        }
    }
}