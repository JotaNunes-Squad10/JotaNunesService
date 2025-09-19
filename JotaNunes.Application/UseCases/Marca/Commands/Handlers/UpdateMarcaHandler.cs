using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Marca.Commands.Requests;
using JotaNunes.Application.UseCases.Marca.Commands.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Marca.Commands.Handlers;
public class UpdateMarcaHandler(
    IDomainService domainService,
    IMarcaRepository repository
) : BaseHandler<Domain.Models.Marca, UpdateMarcaRequest, MarcaResponse, IMarcaRepository>(domainService, repository),
    IRequestHandler<UpdateMarcaRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateMarcaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await UpdateAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateMarcaHandler", "Error updating marca:", e);
            return Response();
        }
    }
}