using Microsoft.EntityFrameworkCore;
using BrokerApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BrokerApi.Connection
{
    public class UserDataContext : IdentityDbContext<User>

    {

        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
