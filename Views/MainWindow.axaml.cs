using Avalonia.Controls;
using SensorList.ViewModels;

namespace SensorList.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        MainWindowViewModel.AppMainWindow = this;
    }
}