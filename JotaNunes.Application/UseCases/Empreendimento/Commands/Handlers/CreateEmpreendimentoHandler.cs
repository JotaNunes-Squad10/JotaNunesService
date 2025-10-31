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
    IEmpreendimentoRepository empreendimentoRepository
) : BaseHandler<EmpreendimentoBase, CreateEmpreendimentoRequest, EmpreendimentoBaseResponse, IEmpreendimentoBaseRepository>(domainService, repository),
    IRequestHandler<CreateEmpreendimentoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(CreateEmpreendimentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id != null)
            {
                var empreendimentoBase = await Repository.GetByIdAsync(request.Id.Value);
                
                if (IsNull(empreendimentoBase)) return Response();

                var nextVersion = empreendimentoBase!.Empreendimentos.Count > 0
                    ? empreendimentoBase.Empreendimentos.Max(x => x.Versao) + 1 : 1;

                var newEmpreendimento = Repository.DomainService.Mapper.Map<Domain.Models.Public.Empreendimento>(
                    request,
                    opt =>
                    {
                        opt.Items["Guid"] = empreendimentoBase.Id;
                        opt.Items["Versao"] = nextVersion;
                    });

                await empreendimentoRepository.InsertAsync(newEmpreendimento);
                await CommitAsync();

                var updatedResponse = await Repository.GetByVersionAsync(empreendimentoBase.Id, nextVersion);
                if (IsNull(updatedResponse)) return Response();

                return Response(Map<EmpreendimentoBaseResponse>(updatedResponse!));
            }
            
            var empreendimentoBaseResponse = await InsertAsync(request);
            
            if (IsNull(empreendimentoBaseResponse)) return Response();
            
            var empreendimento = Repository.DomainService.Mapper.Map<Domain.Models.Public.Empreendimento>(
                request,
                opt =>
                {
                    opt.Items["Guid"] = empreendimentoBaseResponse!.Id;
                    opt.Items["Versao"] = 1L;
                });

            await empreendimentoRepository.InsertAsync(empreendimento);
            await CommitAsync();

            var refreshed = await Repository.GetByIdAsync(empreendimentoBaseResponse!.Id);
            if (IsNull(refreshed)) return Response();

            return Response(Map<EmpreendimentoBaseResponse>(refreshed!));
        }
        catch (Exception e)
        {
            AddError("CreateEmpreendimentoHandler", "Error creating Empreendimento:", e);
            return Response();
        }
    }
}