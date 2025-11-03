using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Handlers;

public class DeleteEmpreendimentoHandler(
    IDomainService domainService,
    IEmpreendimentoBaseRepository repository
) : BaseHandler<Domain.Models.Public.EmpreendimentoBase, DeleteEmpreendimentoRequest, EmpreendimentoResponse, IEmpreendimentoBaseRepository>(domainService, repository),
    IRequestHandler<DeleteEmpreendimentoRequest, DefaultResponse>
{
    public Task<DefaultResponse> Handle(DeleteEmpreendimentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Task.FromResult(Response());
        }
        catch (Exception e)
        {
            AddError("DeleteEmpreendimentoHandler", "Error deleting empreendimento:", e);
            return Task.FromResult(Response());
        }
    }
}
