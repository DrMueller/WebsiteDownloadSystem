using System;
using System.Net;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services.Implementation
{
    internal class WebProxy : IWebProxy
    {
        private readonly WebClient _webClient;
        private bool _disposed;

        public WebProxy(WebClient webClient)
        {
            _webClient = webClient;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public byte[] DownloadData(string url)
        {
            return _webClient.DownloadData(url);
        }

        public string DownloadString(Uri uri)
        {
            return _webClient.DownloadString(uri);
        }

        public FunctionResult<byte[]> TryDownloadingData(Uri uri)
        {
            try
            {
                var data = _webClient.DownloadData(uri);
                return FunctionResult.CreateSuccess(data);
            }
            catch
            {
                return FunctionResult.CreateFailure<byte[]>();
            }
        }

        protected virtual void Dispose(bool disposedByCode)
        {
            if (!_disposed)
            {
                if (disposedByCode)
                {
                    _webClient.Dispose();
                }

                _disposed = true;
            }
        }

        ~WebProxy()
        {
            Dispose(false);
        }
    }
}