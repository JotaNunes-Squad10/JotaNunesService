using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Documents.Version1;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Documents.Version2;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Requests;
using MediatR;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Handlers;

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
            var version = request.EmpreendimentoVersion ?? 0;

            version = version == 0 ? await Repository.GetLastVersionAsync(request.Id) : version;

            var empreendimentoBase = await Repository.GetByVersionAsync(request.Id, version);

            if (IsNull(empreendimentoBase)) return Response();

            var empreendimento = empreendimentoBase!.Empreendimentos.FirstOrDefault(e => e.Versao == version);

            if (IsNull(empreendimento)) return Response();

            var documentoRequest = new DocumentoEmpreendimentoRequest
            {
                Id = empreendimentoBase.Id,
                Nome = empreendimento!.Nome,
                Descricao = empreendimento.Descricao,
                Localizacao = empreendimento.Localizacao,
                TamanhoArea = empreendimento.TamanhoArea,
                Versao = empreendimento.Versao,
                EmpreendimentoTopicos = empreendimentoBase.EmpreendimentoTopicos
            };

            IDocument documentoEmpreendimento;

            switch (request.DocumentVersion)
            {
                case 1:
                    documentoEmpreendimento = new DocumentoEmpreendimento(documentoRequest);
                    break;
                default:
                    documentoEmpreendimento = new DocumentoEmpreendimento2(documentoRequest);
                    break;
            }

            var response = documentoEmpreendimento.GeneratePdf();

            return Response(response);
        }
        catch (Exception e)
        {
            AddError(nameof(GenerateDocumentoEmpreendimentoHandler), "Error generating empreendimento document:", e);
            return Response();
        }
    }
}