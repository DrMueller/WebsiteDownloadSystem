using System.Windows.Controls;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.Views.Interfaces;

namespace Mmu.Wds.WpfUI.Areas.ViewModels
{
    /// <summary>
    ///     Interaction logic for DownloadView.xaml
    /// </summary>
    public partial class DownloadView : UserControl, IViewMap<DownloadViewModel>
    {
        public DownloadView()
        {
            InitializeComponent();
        }
    }
}