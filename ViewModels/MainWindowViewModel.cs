﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using SensorList.DAL;
using SensorList.Views;

namespace SensorList.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ISensorRepository _sensorRepository;
    private ObservableCollection<SensorItemViewModel> _sensors;

    public static Window AppMainWindow;
    public ReactiveCommand<Unit, Unit> AddNewItemCommand { get; }

    public ObservableCollection<SensorItemViewModel> Sensors
    {
        get => _sensors;
        set => this.RaiseAndSetIfChanged(ref _sensors, value);
    }

    public MainWindowViewModel(ISensorRepository sensorRepository)
    {
        _sensorRepository = sensorRepository;
        LoadSensors();

        AddNewItemCommand = ReactiveCommand.Create(AddNewItem);
    }

    private void LoadSensors()
    {
        var sensors = _sensorRepository.GetAllSensors();
        Sensors = new ObservableCollection<SensorItemViewModel>(
            sensors.Select(sensor => new SensorItemViewModel(_sensorRepository, sensor))
            );
        foreach (var sensorVM in Sensors)
        {
            sensorVM.SensorDeleted += OnSensorDeleted;
        }
    }

    private async void AddNewItem()
    {
        var createSensorViewModel = new CreateSensorViewModel(_sensorRepository);
        var createSensorView = new CreateSensorView { ViewModel = createSensorViewModel };
        createSensorView.ViewModel.CloseDialogAction = createSensorView.CloseDialog;
        
        var dialog = new Window
        {
            Content = createSensorView,
            Title = "Add new sensor",
            Width = 300,
            Height = 200,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            CanResize = false
        };
        await dialog.ShowDialog(AppMainWindow);
        
        LoadSensors();
    }

    private void OnSensorDeleted()
    {
        LoadSensors();
    }
}
