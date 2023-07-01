using System;
using Microsoft.EntityFrameworkCore;
using SensorList.Models;

namespace SensorList.DAL;

public class SensorDbContext : DbContext
{
    public DbSet<Sensor> Sensors { get; set; }

    // Remember to create your Connection class with a valid connection string
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(Connection.ConnectionString,
            new MariaDbServerVersion(new Version(Connection.ServerVersion)));
    }
}
