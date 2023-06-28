using System;
using System.Collections.Generic;
using System.Linq;
using SensorList.Models;

namespace SensorList.DAL;

public class SensorRepository : ISensorRepository
{
    private readonly SensorDbContext _dbContext;

    public SensorRepository()
    {
        _dbContext = new SensorDbContext();
    }
    
    public IEnumerable<Sensor> GetAllSensors()
    {
        return _dbContext.Sensors.ToList();
    }

    public List<Sensor> GetSensorByName(string name)
    {
        var query = name.ToLower();
        return _dbContext.Sensors.Where(s => s.Category.Contains(query)).ToList();
    }

    public List<Sensor> GetSensorByCategory(string category)
    {
        var query = category.ToLower();
        return _dbContext.Sensors.Where(s => s.Category.Contains(query)).ToList();
    }

    public void AddSensor(Sensor sensor)
    {
        CheckIsNameOrCategoryNull(sensor);

        if (sensor.Amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(sensor.Amount), "Item amount cannot be negative.");
        }

        _dbContext.Sensors.Add(sensor);
        _dbContext.SaveChanges();
    }

    public void UpdateSensor(Sensor sensor)
    {
        CheckIsNameOrCategoryNull(sensor);
        
        _dbContext.Sensors.Update(sensor);
        _dbContext.SaveChanges();
    }

    public void DeleteSensor(Sensor sensor)
    {
        _dbContext.Sensors.Remove(sensor);
        _dbContext.SaveChanges();
    }

    private static void CheckIsNameOrCategoryNull(Sensor sensor)
    {
        var isNullOrEmpty = string.IsNullOrEmpty(sensor.Name) || string.IsNullOrEmpty(sensor.Category);
        
        if (isNullOrEmpty)
        {
            throw new ArgumentException("Name and Type cannot be null or empty.");
        }
    }
}
