using System;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.Services.Servants;

namespace Mmu.Wds.Logic.Areas.Services.WebsitePartHandler.Implementation
{
    internal class LinksHandler : IWebsitePartHandler
    {
        private readonly IFilePathServant _filePathServant;

        public LinksHandler(IFilePathServant filePathServant)
        {
            _filePathServant = filePathServant;
        }

        public void HandlePart(WebClient client, HtmlDocument htmlDoc, Uri downloadUri, string targetPath)
        {
            var links = htmlDoc.DocumentNode.Descendants().Where(f => f.Name == "link");

            var unwantedLinks = new string[]
            {
                "canonical"
            };

            links = links.Where(f => !unwantedLinks.Contains(f.Attributes.First(f => f.Name == "rel").Value));

            foreach (var link in links)
            {
                var href = link.Attributes.Single(f => f.Name == "href");
                var absolutePath = downloadUri.Scheme + Uri.SchemeDelimiter + downloadUri.Host + href.Value;
                var download = client.DownloadData(absolutePath);

                var filePath = targetPath + "/" + href.Value.Substring(2);
                _filePathServant.SaveData(filePath, download);
            }
        }
    }
}