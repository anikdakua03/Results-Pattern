using FluentValidation.Results;

namespace AllResultsPattern.MyCustomResults;

/// <summary>
/// This class will be responsible extracting validation errors from fluent validation and take the necessary informations.
/// </summary>
public class ValidationError
{
    /// <summary>
    /// The name of the property.
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// The error message
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// The property value that caused the failure.
    /// </summary>
    public object AttemptedValue { get; set; }

    /// <summary>
    /// Custom severity level associated with the failure.
    /// </summary>
    public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;

    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    /// Creates a textual representation of the failure.
    /// </summary>
    /// 
    /// <summary>
    /// Creates a new validation failure.
    /// </summary>
    public ValidationError()
    {

    }

    /// <summary>
    /// Creates a new validation failure.
    /// </summary>
    public ValidationError(string propertyName, string errorMessage) : this(propertyName, errorMessage, null)
    {

    }

    /// <summary>
    /// Creates a new ValidationFailure.
    /// </summary>
    public ValidationError(string propertyName, string errorMessage, object attemptedValue)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
        AttemptedValue = attemptedValue;
    }

    public override string ToString()
    {
        return ErrorMessage;
    }

    /// <summary>
    /// Converts list of ValidationFai;lure to a dictionary based on validation property
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static Dictionary<string, List<ValidationError>> ToDictionary(List<ValidationFailure> errors)
    {
        return  errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(n => new ValidationError() 
                {
                    ErrorMessage = n.ErrorMessage,
                    //PropertyName = n.PropertyName,
                    AttemptedValue = n.AttemptedValue,
                    ErrorCode = n.ErrorCode, 
                    Severity = (ValidationSeverity)n.Severity
                }).ToList()
            );
    }
}

public enum ValidationSeverity
{
    Error,
    Warning,
    Info
}