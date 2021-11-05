using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.Wds.WpfUI.Areas.ViewData;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.UrlAlignment.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Models;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Services.Implementation
{
    public abstract class PartHandlerBase : IWebsitePartHandler
    {
        private readonly IFilePathFactory _filePathFactory;
        private readonly IFileRepository _fileRepo;
        private readonly IUrlAlignmentService _urlAligner;

        protected PartHandlerBase(
            IFileRepository fileRepo,
            IUrlAlignmentService urlAligner,
            IFilePathFactory filePathFactory)
        {
            _fileRepo = fileRepo;
            _urlAligner = urlAligner;
            _filePathFactory = filePathFactory;
        }

        public void HandlePart(
            IWebProxy webProxy,
            HtmlDocument htmlDoc,
            Uri downloadUri,
            string targetPath,
            LinkHandlingOptions linkHandlingOption,
            Action<InformationGridEntryViewData> onNewInfo)
        {
            var pathParts = GetParts(htmlDoc, onNewInfo);

            foreach (var part in pathParts)
            {
                // This indicates that we need no download and can just leave it
                if (part.Value.StartsWith("data:"))
                {
                    continue;
                }

                var absoluteUrlPath = _urlAligner.CreateAbsoluteUrl(downloadUri, part.Value);

                if (linkHandlingOption.DoDownloadLocally)
                {
                    var download = webProxy.DownloadData(absoluteUrlPath);
                    if (download != null)
                    {
                        var savePath = _filePathFactory.CreateAbsoluteSavePath(targetPath, part.Value);
                        part.WriteValue(savePath);
                        _fileRepo.SaveData(savePath, download);
                        PostProcessPart(webProxy, part, absoluteUrlPath, savePath);
                    }
                }
                else
                {
                    part.WriteValue(absoluteUrlPath);
                }
            }
        }

        protected abstract IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc, Action<InformationGridEntryViewData> onNewInfo);

        protected virtual void PostProcessPart(IWebProxy client, WebsitePart part, string absoluteFileUrl, string fileSavePath)
        {
        }
    }
}