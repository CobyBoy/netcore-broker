using Microsoft.AspNetCore.Mvc;
using NETCorekt.Connection;
using NETCorekt.Models;
using NETCorekt.Services;

namespace NETCorekt.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherRepository _weatherRepository;
        public WeatherForecastController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;

        }


        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _weatherRepository.GetAllWeatherForecasts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var weatherForecast =  await _weatherRepository.GetWeatherForecastById(id);
            if(weatherForecast == null)
            {
               return NotFound($"weather with id={id} not found");
            }
            return Ok(weatherForecast);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest("Forecast is null");
            }
            return Ok(await _weatherRepository.CreateWeatherForecast(forecast));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var el = await _weatherRepository.GetWeatherForecastById(id);
                if (el == null) 
                { 
                    return NotFound($"weather with id={id} not found"); 
                }
                return Ok(await _weatherRepository.DeleteWeatherForecastById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}