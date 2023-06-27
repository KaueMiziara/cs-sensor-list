using System;
using System.Reactive;
using ReactiveUI;
using SensorList.DAL;
using SensorList.Models;

namespace SensorList.ViewModels;

public class SensorItemViewModel : ViewModelBase
{
    private Sensor _sensor;
    private readonly ISensorRepository _sensorRepository;

    public event Action SensorDeleted;

    public int Id => _sensor.Id;

    public string Name
    {
        get => _sensor.Name;
        set => _sensor.Name = value;
    }

    public string Category
    {
        get => _sensor.Category;
        set => _sensor.Category = value;
    }

    public int Amount
    {
        get => _sensor.Amount;
        set => _sensor.Amount = value;
    }

    public ReactiveCommand<Unit, Unit> DeleteItemCommand { get; }

    public SensorItemViewModel(ISensorRepository sensorRepository, Sensor sensor)
    {
        _sensorRepository = sensorRepository;
        _sensor = sensor;
        
        DeleteItemCommand = ReactiveCommand.Create(DeleteItem);
    }

    private void DeleteItem()
    {
        _sensorRepository.DeleteSensor(_sensor);
        
        SensorDeleted?.Invoke();
    }
}
