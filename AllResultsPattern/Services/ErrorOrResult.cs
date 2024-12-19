using ErrorOr;

namespace AllResultsPattern.Services;

public class ErrorOrResult : IErrorResult
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public ErrorOr<List<WeatherForecast>> GetWeather()
    {
        var err = new Dictionary<string, object>()
        {
            {"Error Key", "Error description" }
        };
        return Error.Conflict("SomeConflict", "Conflicted description...", err);

        return ErrorOr.Error.Custom(123, "Error.Custom", "Some description", err);
        var weatherList = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToList();

        return weatherList;
    }
}


public interface IErrorResult
{
    public ErrorOr<List<WeatherForecast>> GetWeather();
}