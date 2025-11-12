using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Documents;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Requests;
using MediatR;
using QuestPDF.Fluent;

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
            var version = request.Version ?? await Repository.GetLastVersionAsync(request.Id);

            var empreendimentoBase = await Repository.GetByVersionAsync(request.Id, version);

            if (IsNull(empreendimentoBase)) return Response();

            var empreendimento = empreendimentoBase!.Empreendimentos.FirstOrDefault();

            if (IsNull(empreendimento)) return Response();

            var documentoRequest = new DocumentoEmpreendimentoRequest
            {
                Id = empreendimentoBase.Id,
                Nome = empreendimento!.Nome,
                Descricao = empreendimento.Descricao,
                Localizacao = empreendimento.Localizacao,
                TamanhoArea = empreendimento.TamanhoArea,
                EmpreendimentoTopicos = empreendimentoBase.EmpreendimentoTopicos
            };

            var documentoEmpreendimento = new DocumentoEmpreendimento(documentoRequest);

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