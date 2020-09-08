using System;
using System.Threading.Tasks;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Models;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Services
{
    public interface IDownloadService
    {
        Task DownloadAsync(Uri downloadUri, string targetPath, Credentials credentials, Action<InformationGridEntryViewData> onNewInfo);
    }
}