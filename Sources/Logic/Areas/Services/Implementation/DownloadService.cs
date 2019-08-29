using System;
using System.IO.Abstractions;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.Services.Servants;
using Mmu.Wds.Logic.Areas.Services.WebsitePartHandler;

namespace Mmu.Wds.Logic.Areas.Services.Implementation
{
    internal class DownloadService : IDownloadService
    {
        private readonly ICssAlignmentServant _cssAligner;
        private readonly IFilePathServant _filePathServant;
        private readonly IFileSystem _fileSystem;
        private readonly IWebsitePartHandler[] _partHandlers;

        public DownloadService(
            IFilePathServant filePathServant,
            IWebsitePartHandler[] partHandlers,
            IFileSystem fileSystem,
            ICssAlignmentServant cssAligner)
        {
            _filePathServant = filePathServant;
            _partHandlers = partHandlers;
            _fileSystem = fileSystem;
            _cssAligner = cssAligner;
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

                    _cssAligner.AlignCssFiles(webClient, downloadUri, targetPath);

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