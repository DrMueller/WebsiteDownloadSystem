using System.Windows;
using Mmu.Mlh.WpfCoreExtensions.Areas.Initialization.Orchestration.Models;
using Mmu.Mlh.WpfCoreExtensions.Areas.Initialization.Orchestration.Services;

namespace Mmu.Wds.WpfUI
{
    public partial class App
    {
#pragma warning disable VSTHRD100 // Avoid async void methods
        protected override async void OnStartup(StartupEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            var assembly = typeof(App).Assembly;
            var windowConfig = WindowConfiguration.CreateWithDefaultIcon(assembly, "Website Download System");
            var appConfig = new WpfAppConfiguration(assembly, windowConfig);
            await AppStartService.StartAppAsync(appConfig);
        }
    }
}