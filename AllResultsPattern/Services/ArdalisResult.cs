using Ardalis.Result;
using FluentValidation;

namespace AllResultsPattern.Services;

public class ArdalisResult(IValidator<SampleUserRequest> _validator) : IArdalisResult
{
    public async Task<Result<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest)
    {
        var validationResult = await _validator.ValidateAsync(sampleUserRequest);

        if (validationResult.IsValid == false)
        {
            var validationErrors = validationResult.Errors.Select(a => new ValidationError(a.PropertyName, a.ErrorMessage, a.ErrorCode, (ValidationSeverity)a.Severity)).ToList();

            return Result.Invalid(validationErrors);
        }

        // return Result.NotFound(["Resource not found.", "Resource with Id 2 doesn't exist."]);

        return sampleUserRequest.ToUserResponse();
    }
}

public interface IArdalisResult
{
    public Task<Result<SampleUserResponse>> GetWeather(SampleUserRequest sampleUserRequest);
}