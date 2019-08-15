﻿using System.Windows;
using Mmu.Mlh.WpfCoreExtensions.Areas.Initialization.Orchestration.Models;
using Mmu.Mlh.WpfCoreExtensions.Areas.Initialization.Orchestration.Services;

namespace Mmu.Wds.WpfUI
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            var assembly = typeof(App).Assembly;
            var appConfig = WpfAppConfig.CreateWithDefaultIcon(assembly, "Website Downlad System");
            await AppStartService.StartAppAsync(appConfig);
        }
    }
}