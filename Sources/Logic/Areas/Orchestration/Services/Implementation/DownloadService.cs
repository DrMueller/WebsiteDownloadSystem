using System;
using System.IO.Abstractions;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services;

namespace Mmu.Wds.Logic.Areas.Orchestration.Services.Implementation
{
    internal class DownloadService : IDownloadService
    {
        private readonly IFileRepository _filePathServant;
        private readonly IFileSystem _fileSystem;
        private readonly IWebsitePartHandler[] _partHandlers;

        public DownloadService(
            IFileRepository fileRepo,
            IWebsitePartHandler[] partHandlers,
            IFileSystem fileSystem)
        {
            _filePathServant = fileRepo;
            _partHandlers = partHandlers;
            _fileSystem = fileSystem;
        }

        public async Task DownloadAsync(Uri downloadUri, string targetPath)
        {
            await Task.Run(() =>
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

                    var indexPath = targetPath + @"\index.html";
                    if (!_fileSystem.Directory.Exists(targetPath))
                    {
                        _fileSystem.Directory.CreateDirectory(targetPath);
                    }

                    htmlDoc.Save(indexPath);
                }
            });
        }
    }
}