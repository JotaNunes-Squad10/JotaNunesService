using JotaNunes.Domain.Models;

namespace JotaNunes.Domain.Interfaces;

public interface IUser
{
    long Id { get; }
    string Name { get; }
    string Email { get; }
    Group Group { get; }
}