using FluentValidation.Results;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using Microsoft.Extensions.Logging;

namespace JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;

public class Notifications(ILogger<Notifications> logger, DefaultResponse defaultResponse) : INotifications
{
    public void AddError(string property, string message)
        => defaultResponse.ValidationResult.Errors.Add(new ValidationFailure(property, message));

    public void AddError(ValidationFailure failure)
        => defaultResponse.ValidationResult.Errors.Add(failure);

    public bool HasError()
        => !defaultResponse.ValidationResult.IsValid;

    public void LogErrors()
        => logger.LogError(string.Join("; ", defaultResponse.ValidationResult.Errors.Select(x => x.ErrorMessage)));
}