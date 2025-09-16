using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Topico.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Topico.Commands.Handlers;

public class DeleteTopicoHandler(
    IDomainService domainService,
    ITopicoRepository repository
) : BaseHandler<Domain.Models.Topico, BaseRequest, TopicoResponse, ITopicoRepository>(domainService, repository),
    IRequestHandler<BaseRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(BaseRequest request, CancellationToken cancellationToken)
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