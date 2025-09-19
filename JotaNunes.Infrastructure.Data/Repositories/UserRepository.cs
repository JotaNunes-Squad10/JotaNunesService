using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Services;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationContext applicationContext, IDomainService domainService)
    : BaseRepository<User>(applicationContext, domainService), IUserRepository
{
    public override async Task<List<User>> GetAllAsync()
        => await GetTracking.Include(x => x.Attributes).ToListAsync();
    
    public async Task<User?> GetByIdAsync(Guid id)
        => await GetTracking.Include(x => x.Attributes)
            .FirstOrDefaultAsync(x => x.Id == id);
}