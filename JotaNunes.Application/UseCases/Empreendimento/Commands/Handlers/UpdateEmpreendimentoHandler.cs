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
    IEmpreendimentoRepository repository
) : BaseHandler<Domain.Models.Empreendimento, UpdateEmpreendimentoRequest, EmpreendimentoResponse, IEmpreendimentoRepository>(domainService, repository),
    IRequestHandler<UpdateEmpreendimentoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateEmpreendimentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await UpdateAsync(request));
        }
        catch (Exception e)
        {
            AddError("UpdateEmpreendimentoHandler", "Error updating empreendimento:", e);
            return Response();
        }
    }
}