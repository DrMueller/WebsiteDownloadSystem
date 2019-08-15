using System.Threading.Tasks;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Components.CommandBars.ViewData;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels.Behaviors;

namespace Mmu.Wds.WpfUI.Areas.ViewModels
{
    public class DownloadViewModel : ViewModelBase, INavigatableViewModel, IInitializableViewModel
    {
        private readonly CommandContainer _commandContainer;

        private bool _downloadIsRunning;
        private string _downloadUrl;
        private string _targetPath;
        public CommandsViewData Commands => _commandContainer.Commands;

        public bool DownloadIsRunning
        {
            get => _downloadIsRunning;
            set
            {
                if (_downloadIsRunning != value)
                {
                    _downloadIsRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DownloadUrl
        {
            get => _downloadUrl;
            set
            {
                if (_downloadUrl != value)
                {
                    _downloadUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HeadingDescription => "Download Website";

        public string NavigationDescription => "Download";

        public int NavigationSequence => 1;

        public string TargetPath
        {
            get => _targetPath;
            set
            {
                if (_targetPath != value)
                {
                    _targetPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public DownloadViewModel(CommandContainer commandContainer)
        {
            _commandContainer = commandContainer;
        }

        public async Task InitializeAsync(params object[] initParams)
        {
            await _commandContainer.InitializeAsync(this);
        }
    }
}