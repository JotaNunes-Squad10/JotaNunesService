using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimento.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Documents;
using MediatR;
using QuestPDF.Fluent;

namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Handlers;

public class GenerateDocumentoEmpreendimentoHandler(
    IDomainService domainService,
    IEmpreendimentoBaseRepository empreendimentoRepository
) : BaseHandler<Domain.Models.Public.EmpreendimentoBase, GenerateDocumentoEmpreendimentoRequest, DocumentoEmpreendimentoResponse, IEmpreendimentoBaseRepository>(domainService, empreendimentoRepository),
    IRequestHandler<GenerateDocumentoEmpreendimentoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(GenerateDocumentoEmpreendimentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var empreendimentoBase = await Repository.GetByIdAsync(request.Id);

            if (IsNull(empreendimentoBase)) return Response();

            var empreendimento = request.Version > 0
                ? empreendimentoBase!.Empreendimentos.FirstOrDefault(x => x.Versao == request.Version)
                : empreendimentoBase!.Empreendimentos.MaxBy(x => x.Versao);

            if (IsNull(empreendimento)) return Response();

            var documentoEmpreendimento = new DocumentoEmpreendimento(empreendimento!);

            var response = documentoEmpreendimento.GeneratePdf();

            return Response(response);
        }
        catch (Exception e)
        {
            AddError("GenerateDocumentoEmpreendimentoHandler", "Error generating empreendimento document:", e);
            return Response();
        }
    }
}