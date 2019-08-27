using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.Services.Models;
using Mmu.Wds.Logic.Areas.Services.Servants;

namespace Mmu.Wds.Logic.Areas.Services.WebsitePartHandler.Implementation
{
    internal abstract class PartHandlerBase : IWebsitePartHandler
    {
        private readonly IFilePathFactory _filePathFactory;
        private readonly IFilePathServant _filePathServant;
        private readonly IUrlAlignmentServant _urlAligner;

        public PartHandlerBase(
            IFilePathServant filePathServant,
            IUrlAlignmentServant urlAligner,
            IFilePathFactory filePathFactory)
        {
            _filePathServant = filePathServant;
            _urlAligner = urlAligner;
            _filePathFactory = filePathFactory;
        }

        public void HandlePart(WebClient client, HtmlDocument htmlDoc, Uri downloadUri, string targetPath)
        {
            var pathParts = GetParts(htmlDoc);

            foreach (var part in pathParts)
            {
                var absoluteUrlPath = _urlAligner.CreateAbsolutePath(downloadUri, part.Value);
                var download = client.DownloadData(absoluteUrlPath);
                var savePath = _filePathFactory.CreateAbsoluteSavePath(targetPath, part.Value);
                part.WriteValue(savePath);
                _filePathServant.SaveData(savePath, download);
            }
        }

        protected abstract IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc);
    }
}