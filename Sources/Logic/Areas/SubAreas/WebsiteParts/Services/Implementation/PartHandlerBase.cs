using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;
using Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Models;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services.Implementation
{
    internal abstract class PartHandlerBase : IWebsitePartHandler
    {
        private readonly IFilePathFactory _filePathFactory;
        private readonly IFileRepository _fileRepo;
        private readonly IUrlAlignmentService _urlAligner;

        public PartHandlerBase(
            IFileRepository fileRepo,
            IUrlAlignmentService urlAligner,
            IFilePathFactory filePathFactory)
        {
            _fileRepo = fileRepo;
            _urlAligner = urlAligner;
            _filePathFactory = filePathFactory;
        }

        public void HandlePart(IWebProxy webProxy, HtmlDocument htmlDoc, Uri downloadUri, string targetPath)
        {
            var pathParts = GetParts(htmlDoc);

            foreach (var part in pathParts)
            {
                var absoluteUrlPath = _urlAligner.CreateAbsoluteUrl(downloadUri, part.Value);
                var download = webProxy.DownloadData(absoluteUrlPath);
                var savePath = _filePathFactory.CreateAbsoluteSavePath(targetPath, part.Value);
                part.WriteValue(savePath);
                _fileRepo.SaveData(savePath, download);
                PostProcessPart(webProxy, part, absoluteUrlPath, savePath);
            }
        }

        protected abstract IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc);

        protected virtual void PostProcessPart(IWebProxy client, WebsitePart part, string absoluteFileUrl, string fileSavePath)
        {
        }
    }
}