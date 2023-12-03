using BlazorAppPWA.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppPWA.Server.GamesDBContext
{
    public class GamesDBContext : DbContext
    {
        protected readonly IConfiguration _Configuration;
        public GamesDBContext(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // in memory database used for simplicity, change to a real db for production applications
            optionsBuilder.UseInMemoryDatabase(databaseName:"GamesDb");
        }

        public DbSet<Game> Games { get; set; } 
    }
}
