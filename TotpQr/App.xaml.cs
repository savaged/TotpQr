using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TotpQr.Data;
using TotpQr.Interfaces;

namespace TotpQr;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Ioc.Default.ConfigureServices(new ServiceCollection()
            .AddSingleton<IModelService, ModelService>()
            .AddTransient<ITotpService, TotpService>()
            .AddTransient<TotpUriDataViewModel>()
            .AddTransient<TotpUriDataIndexViewModel>()
            .AddTransient<MainWindow>()
            .BuildServiceProvider());
        base.OnStartup(e);
    }
}
