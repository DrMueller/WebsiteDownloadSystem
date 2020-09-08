using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using HtmlAgilityPack;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.CssHandling.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.UrlAlignment.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Models;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Services.Implementation
{
    public class CssFilesHandler : PartHandlerBase
    {
        private readonly ICssFileRepository _cssFileRepository;
        private readonly IFileRepository _fileRepo;
        private readonly IFileSystem _fileSystem;

        public CssFilesHandler(
            IFileSystem fileSystem,
            IFileRepository fileRepo,
            IUrlAlignmentService urlAligner,
            IFilePathFactory filePathFactory,
            ICssFileRepository cssFileRepository)
            : base(fileRepo, urlAligner, filePathFactory)
        {
            _fileSystem = fileSystem;
            _fileRepo = fileRepo;
            _cssFileRepository = cssFileRepository;
        }

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc, Action<InformationGridEntryViewData> onNewInfo)
        {
            onNewInfo(new InformationGridEntryViewData("Fetching CSS parts.."));

            return htmlDoc.DocumentNode
                .Descendants()
                .Where(f => f.Name == "link")
                .Where(f => f.Attributes.Single(f => f.Name == "rel").Value == "stylesheet")
                .Select(f => f.Attributes.Single(f => f.Name == "href"))
                .Select(attr => new WebsitePart(attr))
                .ToList();
        }

        protected override void PostProcessPart(IWebProxy webProxy, WebsitePart part, string fileUrl, string fileSavePath)
        {
            var cssFile = _cssFileRepository.Parse(fileSavePath, fileUrl);

            cssFile.Urls.ForEach(
                url =>
                {
                    if (!_fileSystem.File.Exists(url.FilePath))
                    {
                        var downloadResult = webProxy.TryDownloadingData(url.Uri);
                        if (downloadResult.IsSuccess)
                        {
                            _fileRepo.SaveData(url.FilePath, downloadResult.Value);
                        }
                    }

                    cssFile.AlignUrlFilePath(url);
                });

            _cssFileRepository.Save(cssFile);
        }
    }
}