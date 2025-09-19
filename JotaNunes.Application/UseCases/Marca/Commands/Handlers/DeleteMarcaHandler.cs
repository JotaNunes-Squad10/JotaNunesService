using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Marca.Commands.Requests;
using JotaNunes.Application.UseCases.Marca.Commands.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Marca.Commands.Handlers;
public class DeleteMarcaHandler(
    IDomainService domainService,
    IMarcaRepository repository
) : BaseHandler<Domain.Models.Marca, DeleteMarcaRequest, MarcaResponse, IMarcaRepository>(domainService, repository),
    IRequestHandler<DeleteMarcaRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(DeleteMarcaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await DeleteAsync(request));
        }
        catch (Exception e)
        {
            AddError("DeleteMarcaHandler", "Error deleting marca:", e);
            return Response();
        }
    }
}