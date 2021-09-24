
using DemoApp.API.Models;
using System.Collections.Generic;

namespace DemoApp.API.Services
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetWeatherForecast();
    }
}