using FluentValidation.Results;

namespace JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;

public interface INotifications
{
    void AddError(string property, string message);
    void AddError(ValidationFailure failure);
    bool HasError();
    void LogErrors();
}