using System;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.Services.Servants;

namespace Mmu.Wds.Logic.Areas.Services.WebsitePartHandler.Implementation
{
    internal class ImagesHandler : IWebsitePartHandler
    {
        private readonly IFilePathServant _filePathServant;

        public ImagesHandler(IFilePathServant filePathServant)
        {
            _filePathServant = filePathServant;
        }

        public void HandlePart(WebClient client, HtmlDocument htmlDoc, Uri downloadUri, string targetPath)
        {
            var images = htmlDoc.DocumentNode.Descendants().Where(f => f.Name == "img");

            foreach (var img in images)
            {
                var src = img.Attributes.Single(f => f.Name == "src");
                var absolutePath = downloadUri.Scheme + Uri.SchemeDelimiter + downloadUri.Host + src.Value;
                var download = client.DownloadData(absolutePath);

                var relativeSavePath = src.Value;
                src.Value = relativeSavePath;

                var filePath = targetPath + "/" + src.Value.Substring(2);
                _filePathServant.SaveData(filePath, download);
            }
        }
    }
}