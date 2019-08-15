using System;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.Services.Servants;

namespace Mmu.Wds.Logic.Areas.Services.WebsitePartHandler.Implementation
{
    internal class ScriptsHandler : IWebsitePartHandler
    {
        private readonly IFilePathServant _filePathServant;

        public ScriptsHandler(IFilePathServant filePathServant)
        {
            _filePathServant = filePathServant;
        }

        public void HandlePart(WebClient client,
                       HtmlDocument htmlDoc,
                       Uri downloadUri,
                       string targetPath)
        {
            var scripts = htmlDoc.DocumentNode
                .Descendants()
                .Where(f => f.Name == "script")
                .Where(f => f.Attributes.Any(f => f.Name == "src"))
                .ToList();

            foreach (var script in scripts)
            {
                var href = script.Attributes.Single(f => f.Name == "src");
                var absolutePath = downloadUri.Scheme + Uri.SchemeDelimiter + downloadUri.Host + href.Value;
                var download = client.DownloadData(absolutePath);

                var filePath = targetPath + "/" + href.Value.Substring(2);
                _filePathServant.SaveData(filePath, download);
            }
        }
    }
}