using System;
using System.Threading.Tasks;

namespace Mmu.Wds.Logic.Areas.Orchestration.Services
{
    public interface IDownloadService
    {
        Task DownloadAsync(Uri downloadUri, string targetPath);
    }
}