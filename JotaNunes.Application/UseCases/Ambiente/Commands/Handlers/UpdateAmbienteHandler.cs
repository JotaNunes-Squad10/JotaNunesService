using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Handlers;

public class UpdateAmbienteHandler(
    IDomainService domainService,
    IAmbienteRepository repository
) : BaseHandler<Domain.Models.Ambiente, UpdateAmbienteRequest, AmbienteResponse, IAmbienteRepository>(domainService, repository),
    IRequestHandler<UpdateAmbienteRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateAmbienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var ambiente = await Repository.GetByIdAsync(request.Id);
            
            if (IsNull(ambiente)) return Response();
            
            return Response(await UpdateAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateAmbienteHandler", "Error updating ambiente:", e);
            return Response();
        }
    }
}