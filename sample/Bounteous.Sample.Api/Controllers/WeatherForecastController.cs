using System.Collections.Generic;
using Bountous.Sample.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bountous.Sample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService service;

    public WeatherForecastController(IWeatherService service)
    {
        this.service = service;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return service.Get();
    }
}