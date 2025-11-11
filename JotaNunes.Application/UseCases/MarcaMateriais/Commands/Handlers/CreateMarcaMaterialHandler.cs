using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;
using JotaNunes.Application.UseCases.MarcaMateriais.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MarcaMateriais.Commands.Handlers;

public class CreateMarcaMaterialHandler(
    IDomainService domainService,
    IMarcaMaterialRepository repository
) : BaseHandler<MarcaMaterial, CreateMarcaMaterialRequest, MarcaMaterialResponse, IMarcaMaterialRepository>(domainService, repository),
    IRequestHandler<CreateMarcaMaterialRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateMarcaMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("CreateMaterialMarcaHandler", "Error creating MaterialMarca:", e);
            return Response();
        }
    }
}