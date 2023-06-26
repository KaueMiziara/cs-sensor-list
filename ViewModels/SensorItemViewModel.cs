using SensorList.Models;

namespace SensorList.ViewModels;

public class SensorItemViewModel : ViewModelBase
{
    private Sensor _sensor;

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

    public SensorItemViewModel(Sensor sensor)
    {
        _sensor = sensor;
    }
}
