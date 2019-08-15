using System;
using System.Threading.Tasks;

namespace Mmu.Wds.Logic.Areas.Services
{
    public interface IDownloadService
    {
        Task DownloadAsync(Uri downloadUri, string targetPath);
    }
}