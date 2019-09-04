using System;
using System.Threading.Tasks;
using Mmu.Wds.Logic.Areas.Orchestration.Services.Servants;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services;

namespace Mmu.Wds.Logic.Areas.Orchestration.Services.Implementation
{
    internal class DownloadService : IDownloadService
    {
        private readonly IFileRepository _filePathServant;
        private readonly IHtmlDocumentServant _htmlDocumentServant;
        private readonly IWebsitePartHandler[] _partHandlers;
        private readonly IWebProxyFactory _webProxyFactory;

        public DownloadService(
            IFileRepository fileRepo,
            IWebsitePartHandler[] partHandlers,
            IWebProxyFactory webProxyFactory,
            IHtmlDocumentServant htmlDocumentServant)
        {
            _filePathServant = fileRepo;
            _partHandlers = partHandlers;
            _webProxyFactory = webProxyFactory;
            _htmlDocumentServant = htmlDocumentServant;
        }

        public async Task DownloadAsync(Uri downloadUri, string targetPath)
        {
            await Task.Run(() =>
            {
                _filePathServant.CleanPath(targetPath);
                using (var webProxy = _webProxyFactory.Create())
                {
                    var htmlDocument = _htmlDocumentServant.CreateDocument(webProxy, downloadUri);
                    foreach (var handler in _partHandlers)
                    {
                        handler.HandlePart(webProxy, htmlDocument, downloadUri, targetPath);
                    }

                    _htmlDocumentServant.SaveDocument(targetPath, htmlDocument);
                }
            });
        }
    }
}