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