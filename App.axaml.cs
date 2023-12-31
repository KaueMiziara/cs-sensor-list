using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SensorList.DAL;
using SensorList.ViewModels;
using SensorList.Views;

namespace SensorList;

public partial class App : Application
{
    private readonly ISensorRepository _sensorRepository = new SensorRepository();
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(_sensorRepository),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
