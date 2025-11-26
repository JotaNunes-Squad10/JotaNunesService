using JotaNunes.Application.UseCases.Base.Commands.Handlers;
using JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;
using JotaNunes.Application.UseCases.Empreendimentos.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Handlers;

public class UpdateEmpreendimentoHandler(
    IDomainService domainService,
    IEmpreendimentoBaseRepository repository,
    IEmpreendimentoRepository empreendimentoRepository,
    IEmpreendimentoTopicoRepository empreendimentoTopicoRepository,
    ITopicoAmbienteRepository topicoAmbienteRepository,
    ITopicoMaterialRepository topicoMaterialRepository,
    IAmbienteItemRepository ambienteItemRepository,
    ILogStatusRepository logStatusRepository
) : BaseHandler<EmpreendimentoBase, UpdateEmpreendimentoRequest, EmpreendimentoBaseResponse, IEmpreendimentoBaseRepository>(domainService, repository),
    IRequestHandler<UpdateEmpreendimentoRequest, DefaultResponse>
{
    public async Task<DefaultResponse> Handle(UpdateEmpreendimentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1 - Obter empreendimento base
            var empreendimentoBase = await Repository.GetByIdAsync(request.Id);

            if (IsNull(empreendimentoBase)) return Response();

            if ((Status)empreendimentoBase!.Status == Status.Cancelado)
                return Response("A atualização não é permitida, pois o empreendimento está cancelado.");

            // 2 - Calcular próxima versão
            var nextVersion = empreendimentoBase!.Empreendimentos.Count > 0
                ? empreendimentoBase.Empreendimentos.Max(x => x.Versao) + 1 : 1;

            var newEmpreendimento = Repository.DomainService.Mapper.Map<Empreendimento>(
                request, opt => {
                    opt.Items["Guid"] = empreendimentoBase.Id;
                    opt.Items["Versao"] = nextVersion;
                });

            // 3 - Registrar nova versão do empreendimento
            await UpdateAsync(empreendimentoBase);
            await empreendimentoRepository.InsertAsync(newEmpreendimento);

            // 4 - Atualizar status
            var currentStatus = empreendimentoBase.LogsStatus.OrderByDescending(ls => ls.DataHoraInclusao).FirstOrDefault();

            if ( currentStatus is not { Status: (long)Status.Pendente })
            {
                var logStatusRequest = new LogStatusRequest
                {
                    EmpreendimentoId = empreendimentoBase.Id,
                    Status = (long)Status.Pendente
                };

                var logStatus = Repository.DomainService.Mapper.Map<LogStatus>(logStatusRequest);
                await logStatusRepository.InsertAsync(logStatus);
            }

            // 5 - Atualizar versões dos componentes
            var topicosToAppendVersion = new List<long>();
            var ambientesToAppendVersion = new List<long>();
            var itensToAppendVersion = new List<long>();
            var materiaisToAppendVersion = new List<long>();

            // 5.1 - Atualizar versões dos topicos
            foreach (var newEt in request.EmpreendimentoTopicos)
            {
                // if empreendimentoBase.EmpreendimentoTopicos already contains et.TopicoId
                // => append new version
                // else
                // => create new
                // Same logic for all components
                var et = empreendimentoBase.EmpreendimentoTopicos.FirstOrDefault(et => et.TopicoId == newEt.TopicoId);

                if (et != null) topicosToAppendVersion.Add(et.Id); // O topico já existe => atualizar versão
                else // O topico ainda não existe => registrar novo
                {
                    newEt.EmpreendimentoId = empreendimentoBase.Id;
                    newEt.Versoes = [nextVersion];
                    var empreendimentoTopico = Repository.DomainService.Mapper.Map<EmpreendimentoTopico>(newEt);
                    await empreendimentoTopicoRepository.InsertAsync(empreendimentoTopico);
                    await CommitAsync();
                    et = empreendimentoBase.EmpreendimentoTopicos.FirstOrDefault(et2 => et2.TopicoId == newEt.TopicoId); // Recupera o empreendimentoTopico recém-criado
                }

                // 5.2 - Atualizar versões dos ambientes
                foreach (var newTa in newEt.TopicoAmbientes)
                {
                    var ta = et!.TopicoAmbientes == null ? null : et.TopicoAmbientes.FirstOrDefault(ta2 => ta2.AmbienteId == newTa.AmbienteId);

                    if (ta != null) ambientesToAppendVersion.Add(ta.Id); // O ambiente já existe => atualizar versão
                    else // O ambiente ainda não existe => registrar novo
                    {
                        newTa.TopicoId = et.Id;
                        newTa.Versoes = [nextVersion];
                        var topicoAmbiente = Repository.DomainService.Mapper.Map<TopicoAmbiente>(newTa);
                        await topicoAmbienteRepository.InsertAsync(topicoAmbiente);
                        await CommitAsync();
                        ta = et.TopicoAmbientes == null ? null : et.TopicoAmbientes.FirstOrDefault(ta2 => ta2.AmbienteId == newTa.AmbienteId); // Recupera o topicoAmbiente recém-criado
                    }

                    // 5.3 - Atualizar versões dos itens
                    foreach (var newAi in newTa.AmbienteItens)
                    {
                        var ai = ta!.AmbienteItens == null ? null : ta.AmbienteItens.FirstOrDefault(ai2 => ai2.ItemId == newAi.ItemId);

                        if (ai != null) itensToAppendVersion.Add(ai.Id); // O item já existe => atualizar versão
                        else // O item ainda não existe => registrar novo
                        {
                            newAi.AmbienteId = ta.Id;
                            newAi.Versoes = [nextVersion];
                            var topicoItem = Repository.DomainService.Mapper.Map<AmbienteItem>(newAi);
                            await ambienteItemRepository.InsertAsync(topicoItem);
                            await CommitAsync();
                        }
                    }
                }

                // 5.4 - Atualizar versões dos materiais
                foreach (var newTm in newEt.TopicoMateriais)
                {
                    var tm = et!.TopicoMateriais == null ? null : et.TopicoMateriais.FirstOrDefault(tm2 => tm2.MaterialId == newTm.MaterialId);

                    if (tm != null) materiaisToAppendVersion.Add(tm.Id); // O material já existe => atualizar versão
                    else // O material ainda não existe => registrar novo
                    {
                        newTm.TopicoId = et.Id;
                        newTm.Versoes = [nextVersion];
                        var topicoMaterial = Repository.DomainService.Mapper.Map<TopicoMaterial>(newTm);
                        await topicoMaterialRepository.InsertAsync(topicoMaterial);
                        await CommitAsync();
                    }
                }
            }

            if (topicosToAppendVersion.Count > 0)
                await empreendimentoRepository.AppendVersionToTopicosAsync(topicosToAppendVersion.ToArray(), nextVersion);

            if (ambientesToAppendVersion.Count > 0)
                await empreendimentoRepository.AppendVersionToAmbientesAsync(ambientesToAppendVersion.ToArray(), nextVersion);

            if (itensToAppendVersion.Count > 0)
                await empreendimentoRepository.AppendVersionToItensAsync(itensToAppendVersion.ToArray(), nextVersion);

            if (materiaisToAppendVersion.Count > 0)
                await empreendimentoRepository.AppendVersionToMateriaissAsync(materiaisToAppendVersion.ToArray(), nextVersion);

            await CommitAsync();

            var updatedResponse = await Repository.GetByVersionAsync(empreendimentoBase.Id, nextVersion);

            if (IsNull(updatedResponse)) return Response();

            return Response(Map<EmpreendimentoBaseResponse>(updatedResponse!));
        }
        catch (Exception e)
        {
            AddError("UpdateEmpreendimentoHandler", "Error updating Empreendimento:", e);
            throw;
        }
    }
}