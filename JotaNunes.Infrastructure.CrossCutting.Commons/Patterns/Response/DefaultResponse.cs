using FluentValidation.Results;

namespace JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;

public class DefaultResponse<T>
{
    public T? Data { get; set; }

    public ValidationResult ValidationResult { get; set; } = new();
}

public class DefaultResponse : DefaultResponse<object> { }