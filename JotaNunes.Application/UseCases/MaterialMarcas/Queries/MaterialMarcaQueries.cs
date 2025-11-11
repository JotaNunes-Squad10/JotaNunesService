using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Application.UseCases.MaterialMarcas.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

namespace JotaNunes.Application.UseCases.MaterialMarcas.Queries;

public class MaterialMarcaQueries(
    IDomainService domainService,
    IMaterialMarcaRepository repository,
    IMarcaRepository marcaRepository,
    IMaterialRepository materialRepository
) : BaseQueries<MaterialMarca, MaterialMarcaResponse, IMaterialMarcaRepository>(domainService, repository), IMaterialMarcaQueries
{
    public async Task<DefaultResponse> GetAllMarcasByMaterialIdAsync(long id)
    {
        var material = await materialRepository.GetByIdWithMarcasAsync(id);

        if (IsNull(material)) return Response();

        var response = Map<MarcasByMaterialResponse>(material!);

        return Response(response);
    }

    public async Task<DefaultResponse> GetAllMateriaisByMarcaIdAsync(long id)
    {
        var marca = await marcaRepository.GetByIdWithMateriaisAsync(id);

        if (IsNull(marca)) return Response();

        var response = Map<MateriaisByMarcaResponse>(marca!);

        return Response(response);
    }
}