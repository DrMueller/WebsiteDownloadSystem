using System;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services
{
    public interface IWebProxy : IDisposable
    {
        byte[] DownloadData(string url);

        string DownloadString(Uri uri);

        FunctionResult<byte[]> TryDownloadingData(Uri uri);
    }
}