using System;
using System.ComponentModel;
using System.Reactive;
using ReactiveUI;
using SensorList.DAL;
using SensorList.Models;

namespace SensorList.ViewModels;

public class SensorItemViewModel : ViewModelBase
{
    private Sensor _sensor;
    private readonly ISensorRepository _sensorRepository;

    public event PropertyChangedEventHandler PropertyChanged;
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
        set
        {
            if (_sensor.Amount != value)
            {
                _sensor.Amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
    }

    public ReactiveCommand<Unit, Unit> DeleteItemCommand { get; }

    public SensorItemViewModel(ISensorRepository sensorRepository, Sensor sensor)
    {
        _sensorRepository = sensorRepository;
        _sensor = sensor;

        UpdateAmountOnPropertyChanged();
        
        DeleteItemCommand = ReactiveCommand.Create(DeleteItem);
    }

    private void UpdateItem()
    {
        _sensorRepository.UpdateSensor(_sensor);
    }

    private void DeleteItem()
    {
        _sensorRepository.DeleteSensor(_sensor);
        
        SensorDeleted?.Invoke();
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void UpdateAmountOnPropertyChanged()
    {
        PropertyChanged += SensorItemViewModel_PropertyChanged;
    }

    private void SensorItemViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Amount))
        {
            UpdateItem();
        }
    }
}
