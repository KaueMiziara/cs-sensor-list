using System.Collections.Generic;
using SensorList.Models;

namespace SensorList.DAL;

public interface ISensorRepository
{
    IEnumerable<Sensor> GetAllSensors();
    List<Sensor> GetSensorByName(string? nameLower);
    List<Sensor> GetSensorByCategory(string? category);
    void AddSensor(Sensor sensor);
    void UpdateSensor(Sensor sensor);
    void DeleteSensor(Sensor sensor);
}
