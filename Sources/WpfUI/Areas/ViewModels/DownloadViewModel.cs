using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Components.CommandBars.ViewData;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels.Behaviors;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;

namespace Mmu.Wds.WpfUI.Areas.ViewModels
{
    public class DownloadViewModel : ViewModelBase, INavigatableViewModel, IInitializableViewModel
    {
        private readonly CommandContainer _commandContainer;
        private bool _downloadIsRunning;
        private bool _downloadLocally;
        private string _downloadUrl;
        private string _password;
        private string _targetPath;
        private string _userName;
        public CommandsViewData Commands => _commandContainer.Commands;

        public bool DownloadIsRunning
        {
            get => _downloadIsRunning;
            set
            {
                if (_downloadIsRunning == value)
                {
                    return;
                }

                _downloadIsRunning = value;
                OnPropertyChanged();
            }
        }

        public bool DownloadLocally
        {
            get => _downloadLocally;
            set
            {
                if (_downloadLocally == value)
                {
                    return;
                }

                _downloadLocally = value;
                OnPropertyChanged();
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
        public ObservableCollection<InformationGridEntryViewData> InformationEntries { get; } = new ObservableCollection<InformationGridEntryViewData>();
        public string NavigationDescription => "Download";
        public int NavigationSequence => 1;

        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                {
                    return;
                }

                _password = value;
                OnPropertyChanged();
            }
        }

        public string TargetPath
        {
            get => _targetPath;
            set
            {
                if (_targetPath == value)
                {
                    return;
                }

                _targetPath = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName == value)
                {
                    return;
                }

                _userName = value;
                OnPropertyChanged();
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