using System.Collections.Generic;
using SensorList.Models;

namespace SensorList.DAL;

public interface ISensorRepository
{
    IEnumerable<Sensor> GetAllSensors();
    Sensor GetSensorById(int id);
    Sensor GetSensorByCategory(string category);
    void AddSensor(Sensor sensor);
    void UpdateSensor(Sensor sensor);
    void DeleteSensor(Sensor sensor);
}
