using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Material.Commands.Requests;
using JotaNunes.Application.UseCases.Material.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Handlers;

public class UpdateMaterialHandler(
    IDomainService domainService,
    IMaterialRepository repository
) : BaseHandler<Domain.Models.Material, UpdateMaterialRequest, MaterialResponse, IMaterialRepository>(domainService, repository),
    IRequestHandler<UpdateMaterialRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await UpdateAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateMaterialHandler", "Error updating material:", e);
            return Response();
        }
    }
}