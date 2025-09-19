using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Keycloak;

namespace JotaNunes.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    new Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
}