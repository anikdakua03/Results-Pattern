namespace AllResultsPattern;

public static class SampleUserRequestExtension
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    /// <summary>
    /// Maps SampleUserRequest to SampleUserResponse
    /// </summary>
    /// <param name="sampleUserRequest"></param>
    /// <returns>A sample use response with weather list object.</returns>
    public static SampleUserResponse ToUserResponse(this SampleUserRequest sampleUserRequest)
    {
        return new SampleUserResponse()
        {
            Name = sampleUserRequest.Name ?? string.Empty,
            Email = sampleUserRequest.Email ?? string.Empty,
            Age = sampleUserRequest.Age,
            WeatherForecasts = Enumerable.Range(1, 2).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList()
        };
    }
}