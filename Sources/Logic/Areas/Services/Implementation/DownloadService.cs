using System;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.Services.Servants;
using Mmu.Wds.Logic.Areas.Services.WebsitePartHandler;

namespace Mmu.Wds.Logic.Areas.Services.Implementation
{
    internal class DownloadService : IDownloadService
    {
        private readonly IFilePathServant _filePathServant;
        private readonly IWebsitePartHandler[] _partHandlers;

        public DownloadService(
            IFilePathServant filePathServant,
            IWebsitePartHandler[] partHandlers)
        {
            _filePathServant = filePathServant;
            _partHandlers = partHandlers;
        }

        public async Task DownloadAsync(Uri downloadUri, string targetPath)
        {
            _filePathServant.CleanPath(targetPath);

            using (var webClient = new WebClient())
            {
                var reply = webClient.DownloadString(downloadUri);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(reply);

                foreach (var handler in _partHandlers)
                {
                    handler.HandlePart(webClient, htmlDoc, downloadUri, targetPath);
                }

                htmlDoc.Save(targetPath + @"\index.html");
            }
        }
    }
}