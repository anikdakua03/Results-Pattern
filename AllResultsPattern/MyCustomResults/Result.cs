using System.Text.Json.Serialization;

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
            || (isSuccess == false && error == null && (errors == null || errors.Count == 0)))
        {
            throw new ArgumentException("Invalid argument", nameof(error));
        }

        if (isSuccess && message is null && messages is null)
        {
            throw new ArgumentException("Both message and messages should not be null.");
        }

        if (isSuccess && message is not null && messages is not null)
        {
            throw new ArgumentException("Cannot have both message and messages together.");
        }

        IsSuccess = isSuccess;
        Message = message;
        Messages = messages;
        Error = error;
        Errors = errors;
    }

    public bool HasErrors => Errors is not null && Errors.Count > 0;

    public bool HasWarning => true;

    public bool HasMessages => Messages is not null && Messages.Count > 0;

    [JsonIgnore]
    public IEnumerable<ResultMessage> AllMessages
    {
        get
        {
            if (Message is not null)
            {
                yield return Message;
            }

            if (Messages is not null)
            {
                foreach (ResultMessage message in Messages)
                {
                    yield return message;
                }
            }
        }
    }

    #region Factory methods

    public static Result Success(string message = "Request successful.") => new(true, ResultMessage.Success(message), null, null, null);

    public static Result SuccessWithWarning(string warningMessage) => new(true, ResultMessage.Warning(warningMessage), null, null, null);

    public static Result SuccessWithInformation(string infoMessage) => new(true, ResultMessage.Information(infoMessage), null, null, null);

    public static Result SuccessWithMessage(ResultMessage message)
    {
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message), "Cannot create successful result with null message");
        }

        return new Result(true, message, null, null, null);
    }

    public static Result SuccessWithMessage(params ResultMessage[] messages)
    {
        if (messages is null)
        {
            throw new ArgumentNullException(nameof(messages), "Cannot create successful result with null message");
        }

        return new Result(true, null, [.. messages], null, null);
    }

    public static Result<TValue> Success<TValue>(TValue value, string message = "Request successful.")
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value), "Cannot create success with null value.");
        }

        return (value, ResultMessage.Success(message));
    }

    public static Result<TValue> SuccessWithWarning<TValue>(TValue value, string warningMessage)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value), "Cannot create success with null value.");
        }

        return (value, ResultMessage.Warning(warningMessage));
    }

    public static Result<TValue> SuccessWithInformation<TValue>(TValue value, string infoMessage)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value), "Cannot create success with null value.");
        }

        return (value, infoMessage);
    }

    public static Result<TValue> SuccessWithMessage<TValue>(TValue value, ResultMessage message)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value), "Cannot create success with null value.");
        }

        if (message is null)
        {
            throw new ArgumentNullException(nameof(message), "Cannot create successful result with null message");
        }

        return (value, message);
    }

    public static Result<TValue> SuccessWithMessage<TValue>(TValue value, params ResultMessage[] messages)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value), "Cannot create success with null value.");
        }

        if (messages is null)
        {
            throw new ArgumentNullException(nameof(messages), "Cannot create successful result with null message");
        }

        return (value, [.. messages]);
    }

    public static Result Failure(Error error) => new(false, null, null, error, null);

    public static Result Failure(List<Error> errors) => new(false, null, null, null, errors);

    #endregion
}
