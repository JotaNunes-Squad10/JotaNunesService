using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class TopicoAmbienteRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<TopicoAmbiente>(applicationContext, domainService), ITopicoAmbienteRepository
{

}