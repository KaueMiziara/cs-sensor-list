using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SensorList.Views;

public partial class CreateSensorView : UserControl
{
    public CreateSensorView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}