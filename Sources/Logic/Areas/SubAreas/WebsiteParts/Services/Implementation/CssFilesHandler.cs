using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using HtmlAgilityPack;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Wds.Logic.Areas.SubAreas.CssHandling.Services;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;
using Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Models;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services.Implementation
{
    internal class CssFilesHandler : PartHandlerBase
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

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc)
        {
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
            var fileUrls = cssFile.Urls.Where(f => f.TargetsFile);

            fileUrls.ForEach(url =>
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