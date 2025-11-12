using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Handlers;

public class CreateEmpreendimentoHandler(
    IDomainService domainService,
    IEmpreendimentoBaseRepository repository,
    IEmpreendimentoRepository empreendimentoRepository,
    ILogStatusRepository logStatusRepository
) : BaseHandler<EmpreendimentoBase, CreateEmpreendimentoRequest, EmpreendimentoBaseResponse, IEmpreendimentoBaseRepository>(domainService, repository),
    IRequestHandler<CreateEmpreendimentoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateEmpreendimentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1 - Registrar empreendimento base
            request.Status = (long)Status.Pendente;

            var empreendimentoBase = await InsertAsync(request);

            if (IsNull(empreendimentoBase)) return Response();

            // 2 - Registrar versao do empreendimento
            var newEmpreendimento = Repository.DomainService.Mapper.Map<Empreendimento>(
                request, opt => {
                    opt.Items["Guid"] = empreendimentoBase!.Id;
                    opt.Items["Status"] = (long)Status.Pendente;
                });
            await empreendimentoRepository.InsertAsync(newEmpreendimento);

            // 3 - Registrar status
            var logStatusRequest = new LogStatusRequest
            {
                EmpreendimentoId = empreendimentoBase!.Id,
                Status = (long)Status.Pendente
            };

            var logStatus = Repository.DomainService.Mapper.Map<LogStatus>(logStatusRequest);
            await logStatusRepository.InsertAsync(logStatus);

            // 4 - Confirmar transações e retornar
            await CommitAsync();

            var response = await Repository.GetByIdAsync(empreendimentoBase.Id);

            if (IsNull(response)) return Response();

            return Response(Map(response!));
        }
        catch (Exception e)
        {
            AddError("CreateEmpreendimentoHandler", "Error creating Empreendimento:", e);
            return Response();
        }
    }
}