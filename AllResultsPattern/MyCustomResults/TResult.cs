using System.Diagnostics.CodeAnalysis;

namespace AllResultsPattern.MyCustomResults;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(bool isSuccess, ResultMessage? message, List<ResultMessage>? messages, TValue? value, Error? error, List<Error>? errors)
        : base(isSuccess, message, messages, error, errors)
    {
        //if (isSuccess && value is null)
        //{
        //    throw new ArgumentNullException(nameof(value), "Value cannot be null for successful results.");
        //}

        //if (isSuccess == false && value is not null)
        //{
        //    throw new ArgumentNullException(nameof(value), "Value should be null or default for unsuccessful results.");
        //}

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
                            throw new ArgumentNullException("Cannot access value on a failed result. Check IsSuccess before accessing value.");

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

    public static implicit operator Result<TValue>(TValue? value)
    {
        if (value is null)
        {
            // OR Create an error that it value is null
            //throw new ArgumentNullException(nameof(value), "Cannot create successful result with null value. Result.Failure with appropriate error instead.");
            return Error.NullValue();
        }

        return new Result<TValue>(true, ResultMessage.Success(), null, value, null, null);
    }

    public static implicit operator Result<TValue>((TValue? Value, ResultMessage Message) ValueWithMessage)
    {
        if (ValueWithMessage.Value is null)
        {
            return Error.NullValue();

            //throw new ArgumentNullException(nameof(ValueWithMessage.Value), "Cannot create successful result with null value. Result.Failure with appropriate error instead.");
        }

        return new Result<TValue>(true, ValueWithMessage.Message, null, ValueWithMessage.Value, null, null);
    }

    public static implicit operator Result<TValue>((TValue? Value, List<ResultMessage> Messages) ValueWithMessages)
    {
        if (ValueWithMessages.Value is null)
        {
            return Error.NullValue();
            //throw new ArgumentNullException(nameof(ValueWithMessages.Value), "Cannot create successful result with null value. Result.Failure with appropriate error instead.");
        }

        return new Result<TValue>(true, null, ValueWithMessages.Messages, ValueWithMessages.Value, null, null);
    }

    public static implicit operator Result<TValue>(Error error)
    {
        return new Result<TValue>(false, ResultMessage.Failure(), null, default, error, null);
    }

    public static implicit operator Result<TValue>(List<Error> errors)
    {
        return new Result<TValue>(false, ResultMessage.Failure("Request unsuccessful with multiple errors."), null, default, null, errors);
    }

    #region Factory methods

    public static Result<TValue> Success(TValue? value)
    {
        return new(true, ResultMessage.Success(), null, value, null, null);
    }

    #endregion
}