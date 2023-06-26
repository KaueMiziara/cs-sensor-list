using Microsoft.EntityFrameworkCore;
using SensorList.Models;

namespace SensorList.DAL;

public class SensorDbContext : DbContext
{
    public DbSet<Sensor> Sensors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=sensors.db");
    }
}
