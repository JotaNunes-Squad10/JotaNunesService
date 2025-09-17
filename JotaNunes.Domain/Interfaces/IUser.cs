namespace JotaNunes.Domain.Interfaces;

public interface IUser
{
    Guid Id { get; }
    string Username { get; }
    string Email { get; }
}