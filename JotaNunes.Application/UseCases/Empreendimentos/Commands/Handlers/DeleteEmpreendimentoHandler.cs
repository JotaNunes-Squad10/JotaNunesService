using JotaNunes.Application.UseCases.Base.Commands.Handlers;
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
) : BaseHandler<Domain.Models.Public.EmpreendimentoBase, DeleteEmpreendimentoRequest, EmpreendimentoBaseResponse, IEmpreendimentoBaseRepository>(domainService, repository),
    IRequestHandler<DeleteEmpreendimentoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(DeleteEmpreendimentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var empreendimento = await Repository.GetByIdAsync(request.Id);

            if (IsNull(empreendimento)) return Response();

            return Response(await DeleteAsync(empreendimento!));
        }
        catch (Exception e)
        {
            AddError(nameof(DeleteEmpreendimentoHandler), "Error deleting empreendimento:", e);
            return Response();
        }
    }
}