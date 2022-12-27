using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETCorekt.Models
{
    [Table("WeatherForeCasts")]
    public class WeatherForecast
    {
        public int id { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "TemperatureC is required")]
        public int TemperatureC { get; set; } = Random.Shared.Next(60);

        public int TemperatureF { get; set; }

        public string? Summary { get; set; } = string.Empty;
    }
}