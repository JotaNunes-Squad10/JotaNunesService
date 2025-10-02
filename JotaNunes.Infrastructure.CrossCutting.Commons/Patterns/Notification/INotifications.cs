using FluentValidation.Results;

namespace JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;

public interface INotifications
{
    void AddError(string property, string message);
    void AddError(ValidationFailure failure);
    void ClearErrors();
    List<string> GetErrors();
    bool HasError();
    void LogErrors();
}