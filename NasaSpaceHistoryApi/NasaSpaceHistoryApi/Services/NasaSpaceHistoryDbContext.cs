using System.Data.Entity;
using NasaSpaceHistoryApi.Models;

namespace NasaSpaceHistoryApi.Services
{
    public class NasaSpaceHistoryDbContext : DbContext
    {
        public NasaSpaceHistoryDbContext()
            : base("name=NasaSpaceHistoryDbContext")
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<History> History { get; set; }
    }
}