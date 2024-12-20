using ErrorOr;
using FluentValidation;

namespace AllResultsPattern.Services;

public class ErrorOrResult(IValidator<SampleUserRequest> _validator) : IErrorResult
{
    public async Task<ErrorOr<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest)
    {
        var validationResult = await _validator.ValidateAsync(sampleUserRequest);

        if (validationResult.IsValid == false)
        {
            var validationErrors = validationResult.Errors
                                    .GroupBy(group => group.PropertyName)
                                    .ToDictionary(grp => grp.Key, grp => (object)grp);

            return Error.Validation(metadata: validationErrors);
        }

        // return Error.Unexpected(description: "Something unexpected happened.");

        return sampleUserRequest.ToUserResponse();
    }
}


public interface IErrorResult
{
    public Task<ErrorOr<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest);
}