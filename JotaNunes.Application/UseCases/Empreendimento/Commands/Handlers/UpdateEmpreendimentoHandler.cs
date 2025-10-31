using JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimento.Responses;
using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;
namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Handlers;

public class UpdateEmpreendimentoHandler(
    IDomainService domainService,
    IEmpreendimentoBaseRepository repository
) : BaseHandler<Domain.Models.Public.EmpreendimentoBase, UpdateEmpreendimentoStatusRequest, EmpreendimentoResponse, IEmpreendimentoBaseRepository>(domainService, repository),
    IRequestHandler<UpdateEmpreendimentoStatusRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateEmpreendimentoStatusRequest statusRequest, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await UpdateAsync(statusRequest));
        }
        catch (Exception e)
        {
            AddError("UpdateEmpreendimentoHandler", "Error updating empreendimento:", e);
            return Response();
        }
    }
}