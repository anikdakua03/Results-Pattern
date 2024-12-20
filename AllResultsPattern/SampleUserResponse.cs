namespace AllResultsPattern;

public class SampleUserResponse
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public List<WeatherForecast> WeatherForecasts { get; set; } = [];
}
