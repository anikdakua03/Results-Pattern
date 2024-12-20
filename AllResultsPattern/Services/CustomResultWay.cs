using AllResultsPattern.MyCustomResults;
using FluentValidation;

namespace AllResultsPattern.Services;

public class CustomResultWay(IValidator<SampleUserRequest> _validator) : ICustomResult
{
    public async Task<Result<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest)
    {
        var validationResult = await _validator.ValidateAsync(sampleUserRequest);

        if (validationResult.IsValid == false)
        {
            var validationErrors = validationResult.Errors.GroupBy(a => a.PropertyName).ToDictionary(key => key.Key, value => (object)value);

            return Error.CustomError("Request.validation", "Sample request is invalid.", ErrorType.Validation, validationErrors);
        }

        // OR Single errors
        // return Error.NotFound(description: "");

        // OR to return multiple errors
        // var errors = new List<Error>()
        // {
        //     Error.CustomError("Request.Validation", "Sample request is invalid.", ErrorType.Validation),
        //     Error.NotFound(),
        //     Error.Conflict()
        // };

        // return errors;

        return sampleUserRequest.ToUserResponse();
    }
}

public interface ICustomResult
{
    public Task<Result<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest);
}