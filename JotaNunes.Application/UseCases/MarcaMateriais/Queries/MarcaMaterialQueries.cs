using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.MarcaMateriais.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.MarcaMateriais.Queries;

public class MarcaMaterialQueries(
    IDomainService domainService,
    IMarcaMaterialRepository repository,
    IMarcaRepository marcaRepository,
    IMaterialRepository materialRepository
) : BaseQueries<MarcaMaterial, MarcaMaterialResponse, IMarcaMaterialRepository>(domainService, repository), IMarcaMaterialQueries
{
    public async Task<DefaultResponse> GetAllGroupByMarcaIdAsync(long marcaId)
    {
        var marcas = await marcaRepository.GetAllWithMateriaisAsync();

        if (IsNull(marcas)) return Response();

        var response = Map<List<MateriaisByMarcaResponse>>(marcas);

        return Response(response);
    }

    public async Task<DefaultResponse> GetAllGroupByMaterialIdAsync(long materialId)
    {
        var materiais = await materialRepository.GetAllWithMarcasAsync();

        if (IsNull(materiais)) return Response();

        var response = Map<List<MarcasByMaterialResponse>>(materiais);

        return Response(response);
    }

    public async Task<DefaultResponse> GetAllMarcasByMaterialIdAsync(long materialId)
    {
        var material = await materialRepository.GetByIdWithMarcasAsync(materialId);

        if (IsNull(material)) return Response();

        var response = Map<MarcasByMaterialResponse>(material!);

        return Response(response);
    }

    public async Task<DefaultResponse> GetAllMateriaisByMarcaIdAsync(long marcaId)
    {
        var marca = await marcaRepository.GetByIdWithMateriaisAsync(marcaId);

        if (IsNull(marca)) return Response();

        var response = Map<MateriaisByMarcaResponse>(marca!);

        return Response(response);
    }
}