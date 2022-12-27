using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using NETCorekt.Models;

namespace NETCorekt.Connection
{
    public class ConnectionBdContext : DbContext

    {

        public ConnectionBdContext(DbContextOptions<ConnectionBdContext> options) : base(options) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
