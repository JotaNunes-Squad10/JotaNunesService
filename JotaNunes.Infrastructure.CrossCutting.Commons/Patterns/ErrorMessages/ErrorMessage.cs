namespace JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.ErrorMessages;

public abstract class ErrorMessage
{
    public static string ErrorPersistData
        => "Error while persisting the data.";
    
    public static string InvalidValue(string property)
        => $"Invalid value for property {property}.";
    
    public static string ListIsNullOrEmpty
        => "The list is empty or null.";
    
    public static string ObjectDuplicated
        => "Already exists an object with the same value.";
    
    public static string ObjectNotFound
        => "The requested object was not found.";
    
    public static string UnexpectedError
        => "Unexpected error.";
    
    public static string UserNotAuthorized
        => "The user is not authorized to access the service.";
    
    public static string UserNotPermission
        => "The user does not have permission to perform this operation.";
}