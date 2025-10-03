using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimento.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Handlers;

public class DeleteEmpreendimentoHandler(
    IDomainService domainService,
    IEmpreendimentoRepository repository
) : BaseHandler<Domain.Models.Public.Empreendimento, DeleteEmpreendimentoRequest, EmpreendimentoResponse, IEmpreendimentoRepository>(domainService, repository),
    IRequestHandler<DeleteEmpreendimentoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(DeleteEmpreendimentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Response(await DeleteAsync(request.Id));
        }
        catch (Exception e)
        {
            AddError("DeleteEmpreendimentoHandler", "Error deleting empreendimento:", e);
            return Response();
        }
    }
}