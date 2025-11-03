using JotaNunes.Application.UseCases.Base.Commands;
using JotaNunes.Application.UseCases.Empreendimento.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimento.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimento.Commands.Handlers;

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
            if (request.Id == null)
            {
                return await RegisterNewEmpreendimento(request);
            }
            return await RegisterNewVersion(request);
        }
        catch (Exception e)
        {
            AddError("CreateEmpreendimentoHandler", "Error creating Empreendimento:", e);
            return Response();
        }
    }

    private async Task<DefaultResponse> RegisterNewEmpreendimento(CreateEmpreendimentoRequest request)
    {
        request.Status = (long)Status.Pendente;
        var empreendimentoBase = await InsertAsync(request);
            
        if (IsNull(empreendimentoBase)) return Response();
        
        var newEmpreendimento = Repository.DomainService.Mapper.Map<Domain.Models.Public.Empreendimento>(
            request, opt => {
                opt.Items["Guid"] = empreendimentoBase!.Id;
                opt.Items["Status"] = (long)Status.Pendente;
                opt.Items["Versao"] = 1;
            });

        var logStatusRequest = new LogStatusRequest
        {
            EmpreendimentoId = empreendimentoBase!.Id,
            Status = (long)Status.Pendente
        };

        var logStatus = Repository.DomainService.Mapper.Map<LogStatus>(logStatusRequest);
        
        await empreendimentoRepository.InsertAsync(newEmpreendimento);
        await empreendimentoRepository.AppendVersionToRelationsAsync(empreendimentoBase!.Id, 1);
        await logStatusRepository.InsertAsync(logStatus);
        await CommitAsync();

        var response = await Repository.GetByIdAsync(empreendimentoBase!.Id);
            
        if (IsNull(response)) return Response();

        return Response(Map(response!));
    }
    
    private async Task<DefaultResponse> RegisterNewVersion(CreateEmpreendimentoRequest request)
    {
        var empreendimentoBase = await Repository.GetByIdAsync(request.Id!.Value);
                
        if (IsNull(empreendimentoBase)) return Response();

        empreendimentoBase!.Status = (long)Status.Pendente;
        await UpdateAsync(empreendimentoBase);

        var nextVersion = empreendimentoBase!.Empreendimentos.Count > 0
            ? empreendimentoBase.Empreendimentos.Max(x => x.Versao) + 1 : 1;
        
        var newEmpreendimento = Repository.DomainService.Mapper.Map<Domain.Models.Public.Empreendimento>(
            request, opt => {
                opt.Items["Guid"] = empreendimentoBase.Id;
                opt.Items["Versao"] = nextVersion;
            });

        var logStatusRequest = new LogStatusRequest
        {
            EmpreendimentoId = empreendimentoBase.Id,
            Status = (long)Status.Pendente
        };

        var logStatus = Repository.DomainService.Mapper.Map<LogStatus>(logStatusRequest);

        await empreendimentoRepository.InsertAsync(newEmpreendimento);
        await empreendimentoRepository.AppendVersionToRelationsAsync(empreendimentoBase.Id, nextVersion);
        await logStatusRepository.InsertAsync(logStatus);
        await CommitAsync();

        var updatedResponse = await Repository.GetByVersionAsync(empreendimentoBase.Id, nextVersion);
                
        if (IsNull(updatedResponse)) return Response();

        return Response(Map<EmpreendimentoBaseResponse>(updatedResponse!));
    }
}