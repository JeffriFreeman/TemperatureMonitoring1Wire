using Microsoft.EntityFrameworkCore;

namespace Monitoring.Model
{
    public class SensorsContext : DbContext
    {
        public DbSet<SensorsTemp> Sensors { get; set; }
        public DbSet<SensorsTempData> SensorsData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=SensorsDB1Wire.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensorsTemp>()
                .HasKey(c => new { c.Id });
            modelBuilder.Entity<SensorsTempData>()
                .HasKey(c => new { c.Id, c.LastGet });
        }
    }
}
