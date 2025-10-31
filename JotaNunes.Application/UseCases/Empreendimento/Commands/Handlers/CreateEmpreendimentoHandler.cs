using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimento.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Handlers;

public class CreateEmpreendimentoHandler(
    IDomainService domainService,
    IEmpreendimentoBaseRepository repository
) : BaseHandler<Domain.Models.Public.EmpreendimentoBase, CreateEmpreendimentoRequest, EmpreendimentoResponse, IEmpreendimentoBaseRepository>(domainService, repository),
    IRequestHandler<CreateEmpreendimentoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateEmpreendimentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var empreendimento = Map(request);
            return Response(await InsertAsync(empreendimento));
        }
        catch (Exception e)
        {
            AddError("CreateEmpreendimentoHandler", "Error creating Empreendimento:", e);
            return Response();
        }
    }
}