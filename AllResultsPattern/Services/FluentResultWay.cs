using FluentResults;
using FluentValidation;

namespace AllResultsPattern.Services;

public class FluentResultWay(IValidator<SampleUserRequest> _validator) : IFluentResultWay
{
    public async Task<Result<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest)
    {
        var validationResult = await _validator.ValidateAsync(sampleUserRequest);

        if (validationResult.IsValid == false)
        {
            var validationErrors = validationResult.Errors
                                    .GroupBy(group => group.PropertyName)
                                    .ToDictionary(grp => grp.Key, grp => (object)grp);

            var error = new Error("Some validation errors.");
            error.WithMetadata(validationErrors);
            return Result.Fail(error);
        }

        // return new Error("Some unexpected happened.");

        return sampleUserRequest.ToUserResponse();
    }
}

public interface IFluentResultWay
{
    public Task<Result<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest);
}