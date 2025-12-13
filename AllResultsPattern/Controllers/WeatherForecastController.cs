using AllResultsPattern.MyCustomResults;
using AllResultsPattern.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllResultsPattern.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IArdalisResult _ardalisResult;
    private readonly IErrorResult _errorResult;
    private readonly IFluentResultWay _fluentResultWay;
    private readonly ICSharpFunctionalResult _cSharpFunctionalResult;
    private readonly ICustomResult _customResult;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IArdalisResult ardalisResult, IErrorResult errorResult, IFluentResultWay fluentResultWay, ICSharpFunctionalResult cSharpFunctionalResult, ICustomResult customResult)
    {
        _logger = logger;
        _ardalisResult = ardalisResult;
        _errorResult = errorResult;
        _fluentResultWay = fluentResultWay;
        _cSharpFunctionalResult = cSharpFunctionalResult;
        _customResult = customResult;
    }

    [HttpGet("ardalis", Name = "GetWeatherForecastFromArdalisResult")]
    public async Task<IActionResult> GetWeatherFromArdalis([FromBody] SampleUserRequest sampleUserRequest)
    {
        var res = await _ardalisResult.GetWeather(sampleUserRequest);

        return Ok(res);
    }

    [HttpGet("error-or", Name = "GetWeatherForecastFromErrorResult")]
    public async Task<IActionResult> GetWeatherFromErrorOr([FromBody] SampleUserRequest sampleUserRequest)
    {
        var res = await _errorResult.GetWeather(sampleUserRequest);

        // return await res.MatchAsync(
        //     res => Ok(res),
        //     err => BadRequest(err));

        if (res.IsError)
        {
            return BadRequest(res);
        }

        return Ok(res);
    }

    [HttpGet("fluent", Name = "GetWeatherForecastFromFluentResult")]
    public async Task<IActionResult> GetWeatherFromFluent([FromBody] SampleUserRequest sampleUserRequest)
    {
        var res = await _fluentResultWay.GetWeather(sampleUserRequest);

        if (res.IsFailed)
        {
            return BadRequest(res.Errors);
        }

        return Ok(res);
    }

    [HttpGet("csharp-func", Name = "GetWeatherForecastFromCSharp")]
    public async Task<IActionResult> GetWeatherFromCSharp([FromBody] SampleUserRequest sampleUserRequest)
    {
        var res = await _cSharpFunctionalResult.GetWeather(sampleUserRequest);

        if (res.IsFailure)
        {
            return BadRequest(res.Error);
        }

        return Ok(res.Value);
    }

    [HttpPost("custom", Name = "GetWeatherForecastFromCustom")]
    public async Task<IActionResult> GetWeatherFromCustom([FromBody] SampleUserRequest sampleUserRequest)
    {
        var res = await _customResult.GetWeather(sampleUserRequest);

        return res.ToHttpResponse();
    }

    [HttpPost("custom-good", Name = "GetGoodWeatherForecastFromCustom")]
    public async Task<IActionResult> GetGoodWeatherFromCustom([FromBody] SampleUserRequest sampleUserRequest)
    {
        var res = await _customResult.GetGoodWeather(sampleUserRequest);

        return res.ToHttpResponse();
    }
}
