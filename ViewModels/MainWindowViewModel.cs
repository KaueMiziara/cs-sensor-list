using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using SensorList.DAL;
using SensorList.Models;
using SensorList.Views;

namespace SensorList.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ISensorRepository _sensorRepository;
    private ObservableCollection<SensorItemViewModel> _sensors;

    public static Window AppMainWindow;
    public ReactiveCommand<Unit, Unit> AddNewItemCommand { get; }
    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<SensorItemViewModel> Sensors
    {
        get => _sensors;
        set => this.RaiseAndSetIfChanged(ref _sensors, value);
    }

    private string _searchName;
    private string _searchCategory;
    
    public string SearchName
    {
        get => _searchName; 
        set => SetProperty(ref _searchName, value, nameof(SearchName));

    }
    
    public string SearchCategory
    {
        get => _searchCategory;
        set => SetProperty(ref _searchCategory, value, nameof(SearchCategory));
    }
    
    private void SetProperty<T>(ref T field, T value, string propertyName)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        
        field = value;
        OnPropertyChanged(propertyName);
        LoadSensors();
    }

    public MainWindowViewModel(ISensorRepository sensorRepository)
    {
        _sensorRepository = sensorRepository;
        LoadSensors();

        AddNewItemCommand = ReactiveCommand.Create(AddNewItem);
    }

    private void LoadSensors()
    {
        var sensors = GetSensor();
        
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

    private IEnumerable<Sensor> GetSensor()
    {
        var isNameNull = string.IsNullOrWhiteSpace(_searchName); 
        var isCategoryNull = string.IsNullOrWhiteSpace(_searchCategory); 
        
        return (isNameNull && isCategoryNull) ?
            _sensorRepository.GetAllSensors() : GetByProperty(isCategoryNull);
    }

    private IEnumerable<Sensor> GetByProperty(bool isCategoryNull)
    {
        return !isCategoryNull
            ? _sensorRepository.GetSensorByCategory(_searchCategory)
            : _sensorRepository.GetSensorByName(_searchName);
    }

    private void OnSensorDeleted()
    {
        LoadSensors();
    }
    
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
