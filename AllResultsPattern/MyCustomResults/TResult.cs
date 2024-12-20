namespace AllResultsPattern.MyCustomResults;

public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result(bool isSuccess, string message, TValue value, Error error, List<Error> errors) : base(isSuccess, message, error, errors)
    {
        _value = value;
    }

    //public TValue Value => IsSuccess ? _value : throw new ArgumentException("Invalid argument where trying to get value when it is not success.", nameof(IsSuccess));

    public TValue Value => IsSuccess ? _value : default;

    public static implicit operator Result<TValue>(TValue value)
    {
        return new Result<TValue>(true, "Request successful.", value, null!, null!);
    }

    public static implicit operator Result<TValue>(Error error)
    {
        return new Result<TValue>(false, "Request unsuccessful.", default!, error, null!);
    }

    public static implicit operator Result<TValue>(List<Error> errors)
    {
        return new Result<TValue>(false, "Request unsuccessful with multiple errors.", default!, null!, errors);
    }
}