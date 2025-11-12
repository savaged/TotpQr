using CommunityToolkit.Mvvm.DependencyInjection;

namespace TotpQr;

public class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        TotpUriDataViewModel = Ioc.Default.GetService<TotpUriDataViewModel>()!;
        TotpUriDataIndexViewModel = Ioc.Default.GetService<TotpUriDataIndexViewModel>()!;
    }

    public async Task LoadAsync() =>
        await TotpUriDataIndexViewModel.LoadIndexAsync();

    public TotpUriDataViewModel TotpUriDataViewModel { get; }

    public TotpUriDataIndexViewModel TotpUriDataIndexViewModel { get; }
}
