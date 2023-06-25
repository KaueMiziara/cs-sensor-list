using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SensorList.Views;

public partial class SensorItemView : UserControl
{
    public SensorItemView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}