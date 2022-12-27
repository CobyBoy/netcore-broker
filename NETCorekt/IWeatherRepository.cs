using NETCorekt.Models;

namespace NETCorekt
{
    public interface IWeatherRepository
    {
        public Task<List<WeatherForecast>> GetAllWeatherForecasts();
        public Task<WeatherForecast> GetWeatherForecastById(int id);
        public Task<List<WeatherForecast>> CreateWeatherForecast(WeatherForecast forecast);
        public Task<WeatherForecast> DeleteWeatherForecastById(int id);
        
    }
}
