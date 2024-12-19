using AllResultsPattern.MyCustomResults;
using FluentValidation;
using FluentValidation.Results;

namespace AllResultsPattern.Services;

public class CustomResultWay(IValidator<SampleUserRequest> _validator) : ICustomResult
{
    private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    //public async Task<Result> GetWeather(SampleUserRequest sampleUserRequest)
    public async Task<Result<List<WeatherForecast>>> GetWeather(SampleUserRequest sampleUserRequest)
    {
        //return Error.Conflict();

        var validationResult = await _validator.ValidateAsync(sampleUserRequest);

        if(validationResult.IsValid == false)
        {
            var validationErros = validationResult.Errors;
            //return Result.Failure(SampleError.SamepleValidationError);
            //return Result.Failure([Error.Custom(0, "Failed", "", new Dictionary<string, object>()
            //{
            //    {"ValidationKey", validationErros}
            //})]);
            var er = ValidationError.ToDictionary(validationErros);

            //return Error.Custom(0, "Custom", "Lets check fluent validation error", er);
            var obj = new Dictionary<string, object>()
            {
                    //{ "key1", "value1" },    // Adding a string value
                    //{ "key2", 123 },          // Adding an integer value
                    //{ "key3", true }          // Adding a boolean value
            };

            //var validationFailedObj = new ValidationFailed(validationErros);  // not present

                //var c = validationErros.GroupBy(a => a.PropertyName)
                //.ToDictionary(
                //    b => b.Key, 
                //    c => c.ToList());

            //obj.Add("validations", er);
            var errors = new List<Error>()
            {
                //Error.Conflict(),
                Error.Failure("Default", "All fluent validation errors..", er),
                //Error.Forbidden(),
                //Error.NotFound(),
                //Error.Validation()
            };
            //return Error.NotFound();

            return errors;
        }

    var weatherList = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToList();

        //return Result.Success(weatherList);
        return weatherList;
    }
}

public interface ICustomResult
{
    public Task<Result<List<WeatherForecast>>> GetWeather(SampleUserRequest sampleUserRequest);
    //public Task<Result> GetWeather(SampleUserRequest sampleUserRequest);
}