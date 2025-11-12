using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;
using JotaNunes.Application.UseCases.MarcaMateriais.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MarcaMateriais.Commands.Handlers;

public class UpdateMarcaMaterialHandler(
    IDomainService domainService,
    IMarcaMaterialRepository repository
) : BaseHandler<MarcaMaterial, UpdateMarcaMaterialRequest, MarcaMaterialResponse, IMarcaMaterialRepository>(domainService, repository),
    IRequestHandler<UpdateMarcaMaterialRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateMarcaMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await UpdateAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateMaterialMarcaHandler", "Error updating MaterialMarca:", e);
            return Response();
        }
    }
}