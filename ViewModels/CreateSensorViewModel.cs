using System;
using System.Reactive;
using ReactiveUI;
using SensorList.DAL;
using SensorList.Models;

namespace SensorList.ViewModels;

public class CreateSensorViewModel : ViewModelBase
{
    private readonly ISensorRepository _sensorRepository;
    
    public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    public ReactiveCommand<Unit, Unit> CloseDialogCommand { get; }
    
    public Action? CloseDialogAction { get; set; }

    public CreateSensorViewModel(ISensorRepository sensorRepository)
    {
        _sensorRepository = sensorRepository;

        CreateCommand = ReactiveCommand.Create(CreateSensor);
        CloseDialogCommand = ReactiveCommand.Create(CloseDialog);
    }

    private string? _name;
    private string? _category;
    private int _amount;

    public string? Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    public string? Category
    {
        get => _category;
        set => this.RaiseAndSetIfChanged(ref _category, value);
    }
    public int Amount
    {
        get => _amount;
        set => this.RaiseAndSetIfChanged(ref _amount, value);
    }

    private void CreateSensor()
    {
        Sensor newSensor = new Sensor()
        {
            Name = _name,
            Category = _category,
            Amount = _amount
        };
        
        _sensorRepository.AddSensor(newSensor);
        
        CloseDialog();
    }

    private void CloseDialog()
    {
        CloseDialogAction?.Invoke();
    }
}
