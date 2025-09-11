using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class MaterialRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<Material>(applicationContext, domainService), IMaterialRepository
{
    
}