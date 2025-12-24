using CitiesManager.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Web.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {

        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(new City()
            {
                CityId = Guid.Parse("{B49BF200-3B7E-4513-9F3A-B271573AFC9D}"),
                CityName = "New York"
            });

            modelBuilder.Entity<City>().HasData(new City()
            {
                CityId = Guid.Parse("{4B0A1FD2-638D-4A83-9693-A45F1450336F}"),
                CityName = "Los Angeles"
            });
        }
    }
}
