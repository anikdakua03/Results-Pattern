using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;

namespace AllResultsPattern.Services;

public class CSharpFunctionalResult(IValidator<SampleUserRequest> _validator) : ICSharpFunctionalResult
{
    public async Task<Result<SampleUserRequest, List<ValidationFailure>>> GetWeather(SampleUserRequest sampleUserRequest)
    {
        var validationResult = await _validator.ValidateAsync(sampleUserRequest);

        if (validationResult.IsValid == false)
        {
            var errorResult = validationResult.Errors;

            return errorResult;
        }

        return sampleUserRequest;
    }
}

public interface ICSharpFunctionalResult
{
    public Task<Result<SampleUserRequest, List<ValidationFailure>>> GetWeather(SampleUserRequest sampleUserRequest);
}