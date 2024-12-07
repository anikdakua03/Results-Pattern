using AllResultsPattern.MyCustomResults;
using AllResultsPattern.Services;
using FluentValidation;
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
    private readonly IValidator<SampleUserRequest> _validator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IArdalisResult ardalisResult, IErrorResult errorResult, IFluentResultWay fluentResultWay, ICSharpFunctionalResult cSharpFunctionalResult, ICustomResult customResult, IValidator<SampleUserRequest> validator)
    {
        _logger = logger;
        _ardalisResult = ardalisResult;
        _errorResult = errorResult;
        _fluentResultWay = fluentResultWay;
        _cSharpFunctionalResult = cSharpFunctionalResult;
        _customResult = customResult;
        _validator = validator;
    }

    [HttpGet("ardalis", Name = "GetWeatherForecastFromArdalisResult")]
    public IActionResult GetWeatherFromArdalis()
    {
        var res = _ardalisResult.GetWeather();

        return Ok(res);
    }

    [HttpGet("erroror", Name = "GetWeatherForecastFromErrorResult")]
    public IActionResult GetWeatherFromErrorOr()
    {
        var res = _errorResult.GetWeather();

        return Ok(res);
    }

    [HttpGet("fluent", Name = "GetWeatherForecastFromFluentResult")]
    public IActionResult GetWeatherFromFluentr()
    {
        var res = _fluentResultWay.GetWeather();

        return Ok(res);
    }

    [HttpGet("csharpfunc", Name = "GetWeatherForecastFromCSharp")]
    public IActionResult GetWeatherFromCSharp()
    {
        var res = _cSharpFunctionalResult.GetWeather();
        var c = res.GetValueOrDefault();
        return Ok(c);
    }

    [HttpPost("custom", Name = "GetWeatherForecastFromCustom")]
    public async Task<IActionResult> GetWeatherFromCustom([FromBody] SampleUserRequest sampleUserRequest)
    {
        var validationRes = await _validator.ValidateAsync(sampleUserRequest);

        //return BadRequest(validationRes);
        var res = await _customResult.GetWeather(sampleUserRequest);

        return res.ToHttpResponse();
    }
}
