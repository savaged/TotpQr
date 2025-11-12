using System.Windows;
using System.Windows.Controls;

namespace TotpQr;

public partial class ToptUriDataIndexView : UserControl
{
    private TotpUriDataIndexViewModel? _vm;
    
    public ToptUriDataIndexView() => InitializeComponent();

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is TotpUriDataIndexViewModel vm)
        {
            _vm = vm;
            _vm.NewModel += OnNewModel;
        }
    }

    private void OnNewModel(object? sender, EventArgs e) => IndexDataGrid.UnselectAll();

}
