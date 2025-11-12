using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Handlers;

public class UpdateAmbienteHandler(
    IDomainService domainService,
    IAmbienteRepository repository
) : BaseHandler<Domain.Models.Public.Ambiente, UpdateAmbienteRequest, AmbienteResponse, IAmbienteRepository>(domainService, repository),
    IRequestHandler<UpdateAmbienteRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateAmbienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await UpdateAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateAmbienteHandler", "Error updating ambiente:", e);
            return Response();
        }
    }
}