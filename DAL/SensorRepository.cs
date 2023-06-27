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

    public Sensor GetSensorById(int id)
    {
        var sensor = _dbContext.Sensors.FirstOrDefault(s => s.Id == id);
        CheckFoundSensor(sensor);

        return sensor;
    }

    public Sensor GetSensorByCategory(string category)
    {
        var sensor = _dbContext.Sensors.FirstOrDefault(s => s.Category == category);
        CheckFoundSensor(sensor);

        return sensor;
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

    private static void CheckFoundSensor(Sensor? sensor)
    {
        if (sensor == null)
        {
            throw new ArgumentException("Sensor not found.");
        }
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