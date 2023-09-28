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
            set => OnPropertyChanged(value, ref _downloadLocally);
        }

        public bool DownloadLocally
        {
            get => _downloadLocally;
            set => OnPropertyChanged(value, ref _downloadLocally);
        }

        public string DownloadUrl
        {
            get => _downloadUrl;
            set => OnPropertyChanged(value, ref _downloadUrl);
        }

        public string HeadingDescription => "Download Website";
        public ObservableCollection<InformationGridEntryViewData> InformationEntries { get; } = new ObservableCollection<InformationGridEntryViewData>();
        public string NavigationDescription => "Download";
        public int NavigationSequence => 1;

        public string Password
        {
            get => _password;
            set => OnPropertyChanged(value, ref _password);
        }

        public string TargetPath
        {
            get => _targetPath;
            set => OnPropertyChanged(value, ref _targetPath);
        }

        public string UserName
        {
            get => _userName;
            set => OnPropertyChanged(value, ref _userName);
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