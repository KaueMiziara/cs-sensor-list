using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using SensorList.DAL;

namespace SensorList.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ISensorRepository _sensorRepository;
    private List<SensorItemViewModel> _sensors;

    public List<SensorItemViewModel> Sensors
    {
        get => _sensors;
        set => this.RaiseAndSetIfChanged(ref _sensors, value);
    }

    public MainWindowViewModel(ISensorRepository sensorRepository)
    {
        _sensorRepository = sensorRepository;
        LoadSensors();
    }

    private void LoadSensors()
    {
        var sensors = _sensorRepository.GetAllSensors();
        Sensors = new List<SensorItemViewModel>(
            sensors.Select(sensor => new SensorItemViewModel(sensor))
            );
    }
}
