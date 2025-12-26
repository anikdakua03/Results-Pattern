using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace AllResultsPattern.MyCustomResults;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, ResultMessage? message, List<ResultMessage>? messages, Error? error, List<Error>? errors)
        : base(isSuccess, message, messages, error, errors)
    {
        if (isSuccess && value is null)
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null for successful results.");
        }

        _value = value;
    }

    //[NotNull]
    //public TValue Value
    //{
    //    get
    //    {
    //        if(IsSuccess == false)
    //        {
    //            throw new ArgumentNullException("Cannot access value on a failed result. Check IsSuccess before accessing value.");
    //        }

    //        return _value!; // safe use of "!" operator
    //    }
    //}

    // OR

    [NotNull]
    public TValue Value => IsSuccess ?
                           _value! :
                            //throw new ArgumentNullException("Cannot access value on a failed result. Check IsSuccess before accessing value.") // not thowing, so that error can be deserialized while returning from API as error result
                            default!;
    [JsonIgnore]
    public bool HasValue => IsSuccess && _value is not null;

    public bool TryGetValue(out TValue value)
    {
        if (IsSuccess && _value is not null)
        {
            value = _value;

            return true;
        }

        // other wise default
        value = default!;

        return false;
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value), "Cannot create successful result with null value. Result.Failure with appropriate error instead.");
        }

        return new Result<TValue>(value, true, ResultMessage.Success(), null, null, null);
    }

    public static implicit operator Result<TValue>((TValue Value, ResultMessage Message) ValueWithMessage)
    {
        if (ValueWithMessage.Value is null)
        {
            throw new ArgumentNullException(nameof(ValueWithMessage.Value), "Cannot create successful result with null value. Result.Failure with appropriate error instead.");
        }

        return new Result<TValue>(ValueWithMessage.Value, true, ValueWithMessage.Message, null, null, null);
    }

    public static implicit operator Result<TValue>((TValue Value, List<ResultMessage> Messages) ValueWithMessages)
    {
        if (ValueWithMessages.Value is null)
        {
            throw new ArgumentNullException(nameof(ValueWithMessages.Value), "Cannot create successful result with null value. Result.Failure with appropriate error instead.");
        }

        return new Result<TValue>(ValueWithMessages.Value, true, null, ValueWithMessages.Messages, null, null);
    }

    public static implicit operator Result<TValue>(Error error)
    {
        return new Result<TValue>(default, false, null, null, error, null);
    }

    public static implicit operator Result<TValue>(List<Error> errors)
    {
        return new Result<TValue>(default, false, null, default, null, errors);
    }
}