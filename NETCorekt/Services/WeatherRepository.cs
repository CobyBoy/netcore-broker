using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCorekt.Connection;
using NETCorekt.Models;
using System;

namespace NETCorekt.Services
{
    public class WeatherRepository : IWeatherRepository
    {
        private static readonly string[] Summaries = new[]
        {
        "Freez", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hoooot", "Sweltering", "Scorching"
        };

        private readonly ConnectionBdContext _context;

        public WeatherRepository(ConnectionBdContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherForecast>> GetAllWeatherForecasts()
        {
            var list = await _context.WeatherForecasts.ToListAsync();
            
            return list;
        }

        public async Task<List<WeatherForecast>> CreateWeatherForecast(WeatherForecast forecast)
        {
            forecast.TemperatureF = 32 + forecast.TemperatureC * 9/5;
            var s =_context.WeatherForecasts.Add(forecast);
            await _context.SaveChangesAsync();
            return await _context.WeatherForecasts.ToListAsync();

        }

        public async Task<WeatherForecast> DeleteWeatherForecastById(int id)
        {
          
            try
            {
                var el = await GetWeatherForecastById(id);
                if (el != null)
                {
                    _context.WeatherForecasts.Remove(el);
                    await _context.SaveChangesAsync();
                    return el;
                }
                
               
            } catch(Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<WeatherForecast> GetWeatherForecastById(int id)
        {
            return await _context.WeatherForecasts.FirstOrDefaultAsync(w => w.id == id);
        }
    }
}
