using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Handlers;

public class UpdateEmpreendimentoStatusHandler(
    IDomainService domainService,
    IEmpreendimentoBaseRepository repository,
    ILogStatusRepository logStatusRepository
) : BaseHandler<EmpreendimentoBase, UpdateEmpreendimentoStatusRequest, EmpreendimentoBaseResponse, IEmpreendimentoBaseRepository>(domainService, repository),
    IRequestHandler<UpdateEmpreendimentoStatusRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateEmpreendimentoStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var empreendimentoBase = await Repository.GetByIdAsync(request.Id);

            if (IsNull(empreendimentoBase)) return Response();

            if (empreendimentoBase!.Status == request.Status) return Response();

            empreendimentoBase.Status = request.Status;

            var logStatusRequest = new LogStatusRequest
            {
                EmpreendimentoId = empreendimentoBase.Id,
                Status = empreendimentoBase.Status
            };

            var logStatus = Repository.DomainService.Mapper.Map<LogStatus>(logStatusRequest);
            await logStatusRepository.InsertAsync(logStatus);
            await CommitAsync();

            await UpdateAsync(empreendimentoBase);

            var response = await Repository.GetByIdAsync(empreendimentoBase.Id);

            return Response(Map(response!));
        }
        catch (Exception e)
        {
            AddError("UpdateMarcaHandler", "Error updating marca:", e);
            return Response();
        }
    }
}