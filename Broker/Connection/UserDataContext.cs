using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using BrokerApi.Models;

namespace BrokerApi.Connection
{
    public class UserDataContext : DbContext

    {

        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
