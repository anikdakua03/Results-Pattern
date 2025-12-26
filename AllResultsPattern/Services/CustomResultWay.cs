using AllResultsPattern.MyCustomResults;
using FluentValidation;

namespace AllResultsPattern.Services;

public class CustomResultWay : ICustomResult
{
    private readonly IValidator<SampleUserRequest> _validator;
    private readonly TimeProvider _timeProvider;

    public CustomResultWay(IValidator<SampleUserRequest> validator, TimeProvider timeProvider)
    {
        _validator = validator;
        _timeProvider = timeProvider;
    }

    public async Task<Result<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest)
    {
        var f = _timeProvider.GetUtcNow().DateTime;

        var validationResult = await _validator.ValidateAsync(sampleUserRequest);

        if (validationResult.IsValid == false)
        {
            var validationErrors = validationResult.Errors.GroupBy(a => a.PropertyName).ToDictionary(key => key.Key, value => (object)value);

            return Error.CustomError("Request.validation", "Sample request is invalid.", ErrorType.Validation, validationErrors);
        }

        // OR Single errors
        // return Error.NotFound(description: "");

        // OR to return multiple errors
        List<Error> errors = [
            Error.CustomError("Request.Validation", "Sample request is invalid.", ErrorType.Validation),
            Error.NotFound(),
            Error.Conflict()
        ];

        return errors;
    }

    public async Task<Result<SampleUserResponse>> GetGoodWeather(SampleUserRequest sampleUserRequest)
    {
        var validationResult = await _validator.ValidateAsync(sampleUserRequest);

        if (validationResult.IsValid == false)
        {
            var validationErrors = validationResult.Errors.GroupBy(a => a.PropertyName).ToDictionary(key => key.Key, value => (object)value);

            return Error.CustomError("Request.validation", "Sample request is invalid.", ErrorType.Validation, validationErrors);
        }

        SampleUserResponse response = sampleUserRequest.ToUserResponse();

        // Invalid
        //return null;

        // simple message
        //return response;

        // suceess with some value
        //return (response, ResultMessage.Warning("Trying to send null as value"));

        // success with muliple messages
        //return (response, [
        //    ResultMessage.Success("Success messages."),
        //    ResultMessage.Warning("Some warning"),
        //    ResultMessage.Information("Some information")
        //]);

        // error
        //return SampleError.SamepleValidationError;

        // OR to return multiple errors
        List<Error> errors = [
            Error.CustomError("Request.Validation", "Sample request is invalid.", ErrorType.Validation),
            Error.NotFound(),
            Error.Conflict()
        ];

        return errors;
    }
}

public interface ICustomResult
{
    Task<Result<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest);

    Task<Result<SampleUserResponse>> GetGoodWeather(SampleUserRequest sampleUserRequest);
}