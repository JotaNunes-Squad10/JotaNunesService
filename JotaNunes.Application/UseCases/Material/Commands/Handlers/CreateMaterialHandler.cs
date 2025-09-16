using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Handlers;

public class CreateMaterialHandler(
    IDomainService domainService,
    IMaterialRepository repository
) : BaseHandler<Domain.Models.Material, CreateMaterialRequest, MaterialResponse, IMaterialRepository>(domainService, repository),
    IRequestHandler<CreateMaterialRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await InsertAsync(request));
        }
        catch (Exception e)
        {
            AddError("CreateMaterialmHandler", "Error creating material:", e);
            return Response();
        }
    }
}