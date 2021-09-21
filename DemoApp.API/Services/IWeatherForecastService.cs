
using DemoApp.API.Models;

namespace DemoApp.API.Services
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetWeatherForecast();
    }
}