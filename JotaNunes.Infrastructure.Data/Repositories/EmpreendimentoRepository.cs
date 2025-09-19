using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Public;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class EmpreendimentoRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<Empreendimento>(applicationContext, domainService), IEmpreendimentoRepository
{
    
}