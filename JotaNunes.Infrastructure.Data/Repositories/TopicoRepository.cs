using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class TopicoRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<Topico>(applicationContext, domainService), ITopicoRepository
{
    
}