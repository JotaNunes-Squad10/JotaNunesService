using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Marca.Commands.Requests;
using JotaNunes.Application.UseCases.Marca.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Marca.Commands.Handlers;

public class CreateMarcaHandler(
    IDomainService domainService,
    IMarcaRepository repository
) : BaseHandler<Domain.Models.Marca, CreateMarcaRequest, MarcaResponse, IMarcaRepository>(domainService, repository),
    IRequestHandler<CreateMarcaRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateMarcaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("CreateMarcaHandler", "Error creating Marca:", e);
            return Response();
        }
    }
}