namespace AllResultsPattern.MyCustomResults;

public class Result
{
    public bool IsSuccess { get; }

    public ResultMessage? Message { get; }

    public List<ResultMessage>? Messages { get; }

    public Error? Error { get; }

    public List<Error>? Errors { get; }

    protected Result(bool isSuccess, ResultMessage? message, List<ResultMessage>? messages, Error? error, List<Error>? errors)
    {
        if ((isSuccess && (error != null || (errors != null && errors.Count > 0)))
            || (!isSuccess && error == null && (errors == null || errors.Count == 0)))
        {
            throw new ArgumentException("Invalid argument", nameof(error));
        }

        if(message is null && (messages is null || messages.Count == 0))
        {
            throw new ArgumentException("Both message and messages should nor be null.");
        }

        if (message is not null && (messages is not null && messages.Count > 0))
        {
            throw new ArgumentException("Cannot have both message and messages together.");
        }

        IsSuccess = isSuccess;
        Message = message;
        Messages = messages;
        Error = error;
        Errors = errors;
    }

    public bool HasMultipleErrors => Errors is not null && Errors.Count > 0;

    #region Factory methods

    public static Result Success() => new(true,ResultMessage.Success(), null, null, null);

    public static Result SuccessWithWarning(string warningMessage) => new(true, ResultMessage.Warning(warningMessage), null, null, null);

    public static Result SuccessWithInformation(string infoMessage) => new(true, ResultMessage.Information(infoMessage), null, null, null);

    public static Result Failure(Error error) => new(false, ResultMessage.Failure(), null, error, null);

    public static Result Failure(List<Error> errors) => new(false, ResultMessage.Failure("Request unsuccessful with many errors."), null, null, errors);

    #endregion
}
