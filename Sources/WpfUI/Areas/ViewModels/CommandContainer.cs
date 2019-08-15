using System;
using System.Threading.Tasks;
using Mmu.Mlh.WpfCoreExtensions.Areas.Aspects.InformationHandling.Models;
using Mmu.Mlh.WpfCoreExtensions.Areas.Aspects.InformationHandling.Services;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Commands;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Components.CommandBars.ViewData;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.ViewModelCommands;
using Mmu.Wds.Logic.Areas.Services;

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
                return new ViewModelCommand("Download!",
                    new RelayCommand(async () =>
                    {
                        try
                        {
                            _informationPublisher.Publish(InformationEntry.CreateInfo("Downloading..", true));
                            await _downloadService.DownloadAsync(
                                new Uri(_context.DownloadUrl),
                                _context.TargetPath);
                        }
                        finally
                        {
                            _informationPublisher.Publish(InformationEntry.CreateSuccess("Download finished!", false, 5));
                        }
                    }, CanDownloadWebsite));
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

            _context.DownloadUrl = "https://www.bfh.ch/en/research/reference-projects/peropa/";
            _context.TargetPath = @"C:\Users\Matthias\Desktop\Work\Html";

            Commands = new CommandsViewData(DownloadWebsite);
            return Task.CompletedTask;
        }

        private bool CanDownloadWebsite()
        {
            return !_context.DownloadIsRunning
                && !string.IsNullOrEmpty(_context.DownloadUrl)
                && !string.IsNullOrEmpty(_context.TargetPath);
        }
    }
}