namespace AllResultsPattern.MyCustomResults;

public class Result
{
    public bool IsSuccess { get; }

    public string Message { get; }

    public Error Error { get; }

    public List<Error> Errors { get; }

    protected Result(bool isSuccess, string message, Error error, List<Error> errors)
    {
        #region Causing problem , unable to understand why
        //if((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
        //{
        //    throw new ArgumentException("Invalid argument", nameof(error));
        //}
        #endregion

        IsSuccess = isSuccess;
        Message = message;
        Error = error;
        Errors = errors;
    }

    public bool HasMultipleErrors => Errors is not null && Errors.Count > 0;

    public static Result Success() => new(true,"Request successful.", null, null);

    public static Result<TValue> Success<TValue>(TValue value) => new(true, "Request successful.", value, null, null);

    public static Result Failure(Error error) => new(false, "Request unsuccessful.", error, null);

    public static Result Failure(List<Error> errors) => new(false, "Request unsuccessful.", null, errors);
}
