using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase {
    private static readonly string[] Summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
        "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get() {
        _logger.LogInformation(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        return Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    /// <summary>
    ///     Post测试
    ///     <para>
    ///         获取传入，添加时间字符串直接返回
    ///     </para>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="singletonService">注入服务</param>
    /// <returns></returns>
    [HttpPost]
    public string Post([FromBody] string data, [FromServices] SingletonService singletonService) {
        var str =
            $"{singletonService.AddOne()} {data} {DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
        _logger.LogInformation(str);
        return str;
    }
}