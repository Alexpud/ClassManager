using FluentResults;

namespace ClassManager.Business.Errors;

public class ValidationError : Error
{
    public ValidationError(string errorMessage) : base(errorMessage)
    {
        
    }
}
