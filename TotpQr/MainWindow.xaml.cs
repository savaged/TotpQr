using System.Windows;

namespace TotpQr;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private async void OnWindowLoaded(object sender, RoutedEventArgs e) =>
        await ((MainWindowViewModel)DataContext).LoadAsync();
}