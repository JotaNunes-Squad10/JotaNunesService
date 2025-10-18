using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Handlers;

public class CreateAmbienteHandler(
    IDomainService domainService,
    IAmbienteRepository repository,
    ITopicoRepository topicoRepository
) : BaseHandler<Domain.Models.Public.Ambiente, CreateAmbienteRequest, AmbienteResponse, IAmbienteRepository>(domainService, repository),
    IRequestHandler<CreateAmbienteRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateAmbienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var topico = topicoRepository.GetByIdAsync(request.TopicoId);

            if (IsNull(topico)) return Response();
            
            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("CreateAmbienteHandler", "Error creating ambiente:", e);
            return Response();
        }
    }
}