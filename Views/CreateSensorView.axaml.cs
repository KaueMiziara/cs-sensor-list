using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using SensorList.DAL;
using SensorList.ViewModels;

namespace SensorList.Views;

public partial class CreateSensorView : UserControl
{
    public CreateSensorViewModel ViewModel
    {
        get => DataContext as CreateSensorViewModel;
        set => DataContext = value;
    }
    
    public CreateSensorView()
    {
        InitializeComponent();
    }

    private void InitializeComponent(ISensorRepository sensorRepository)
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void CloseDialog()
    {
        Window parentWindow = this.FindLogicalAncestorOfType<Window>();
        parentWindow.Close();
    }
}
