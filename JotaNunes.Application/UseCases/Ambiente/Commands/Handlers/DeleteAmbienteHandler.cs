using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Handlers;

public class DeleteAmbienteHandler(
    IDomainService domainService,
    IAmbienteRepository repository
) : BaseHandler<Domain.Models.Ambiente, BaseRequest, AmbienteResponse, IAmbienteRepository>(domainService, repository),
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
            AddError("DeleteAmbienteHandler", "Error deleting ambiente:", e);
            return Response();
        }
    }
}