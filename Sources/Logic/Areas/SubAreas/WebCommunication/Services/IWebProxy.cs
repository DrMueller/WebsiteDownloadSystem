using System;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services
{
    internal interface IWebProxy : IDisposable
    {
        byte[] DownloadData(string url);

        string DownloadString(Uri uri);

        FunctionResult<byte[]> TryDownloadingData(Uri uri);
    }
}