using Ardalis.Result;

namespace AllResultsPattern.Services;

public class ArdalisResult : IArdalisResult
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Result<WeatherForecast[]> GetWeather()
    {
        //var errors = new ValidationError[]
        //{ 
        //    new("Error.Validation", "Some error happened", "ErrorCode", ValidationSeverity.Error),
        //    new("Error.Validation2", "Some error 2 happened", "ErrorCode", ValidationSeverity.Info),
        //    new("Error.Validation3", "Some error 3 happened", "ErrorCode", ValidationSeverity.Warning),
        //};

        //return Result.Invalid(errors);

        return Result.Conflict(["Some conflicted error", "Another conflicted error"]);

        var weatherList = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return weatherList;
    }
}

public interface IArdalisResult
{
    public Result<WeatherForecast[]> GetWeather();
}