using System;
using System.Threading.Tasks;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Models;
using Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Services.Servants;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Services;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Services.Implementation
{
    public class DownloadService : IDownloadService
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

        public async Task DownloadAsync(Uri downloadUri, string targetPath, Credentials credentials, Action<InformationGridEntryViewData> onNewInfo)
        {
            await Task.Run(
                () =>
                {
                    _filePathServant.CleanPath(targetPath);
                    using var webProxy = _webProxyFactory.Create(credentials);

                    onNewInfo(new InformationGridEntryViewData("Downloading document.."));
                    var htmlDocument = _htmlDocumentServant.CreateDocument(webProxy, downloadUri);
                    foreach (var handler in _partHandlers)
                    {
                        handler.HandlePart(webProxy, htmlDocument, downloadUri, targetPath, onNewInfo);
                    }

                    _htmlDocumentServant.SaveDocument(targetPath, htmlDocument);
                });
        }
    }
}