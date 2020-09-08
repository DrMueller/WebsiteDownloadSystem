using System;
using System.Threading.Tasks;
using System.Windows;
using Mmu.Mlh.WpfCoreExtensions.Areas.Aspects.ApplicationInformations.Models;
using Mmu.Mlh.WpfCoreExtensions.Areas.Aspects.ApplicationInformations.Services;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Commands;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Components.CommandBars.ViewData;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.ViewModelCommands;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Models;
using Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Services;

namespace Mmu.Wds.WpfUI.Areas.ViewModels
{
    public class CommandContainer : IViewModelCommandContainer<DownloadViewModel>
    {
        private readonly IDownloadService _downloadService;
        private readonly IInformationPublisher _informationPublisher;
        private DownloadViewModel _context;
        public CommandsViewData Commands { get; private set; }

        private IViewModelCommand DownloadWebsite
        {
            get
            {
                return new ViewModelCommand(
                    "Download!",
                    new RelayCommand(
                        async () =>
                        {
                            _informationPublisher.Publish(InformationEntry.CreateInfo("Downloading..", true));

                            var credentials = new Credentials(_context.UserName, _context.Password);
                            await _downloadService.DownloadAsync(
                                new Uri(_context.DownloadUrl),
                                _context.TargetPath,
                                credentials,
                                OnNewInfo);

                            _informationPublisher.Publish(InformationEntry.CreateSuccess("Download finished!", false, 5));
                        },
                        CanDownloadWebsite));
            }
        }

        public CommandContainer(
            IInformationPublisher informationPublisher,
            IDownloadService downloadService)
        {
            _informationPublisher = informationPublisher;
            _downloadService = downloadService;
        }

        public Task InitializeAsync(DownloadViewModel context)
        {
            _context = context;

            _context.DownloadUrl = "http://www.google.ch";
            _context.TargetPath = @"C:\Users\mlm\Desktop\Stuff\Privat\HTML";
            _context.UserName = "test";
            _context.Password = "test2";

            Commands = new CommandsViewData(DownloadWebsite);
            return Task.CompletedTask;
        }

        private bool CanDownloadWebsite()
        {
            return !_context.DownloadIsRunning
                && !string.IsNullOrEmpty(_context.DownloadUrl)
                && !string.IsNullOrEmpty(_context.TargetPath);
        }

        private void OnNewInfo(InformationGridEntryViewData data)
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    _context.InformationEntries.Insert(0, data);
                });
        }
    }
}