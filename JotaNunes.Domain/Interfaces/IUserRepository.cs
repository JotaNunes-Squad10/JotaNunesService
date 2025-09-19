using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Keycloak;

namespace JotaNunes.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByIdAsync(Guid id);
}