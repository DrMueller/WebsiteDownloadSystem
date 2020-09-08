using System;
using System.IO.Abstractions;
using HtmlAgilityPack;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Services.Servants.Implementation
{
    public class HtmlDocumentServant : IHtmlDocumentServant
    {
        private readonly IFileSystem _fileSystem;

        public HtmlDocumentServant(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public HtmlDocument CreateDocument(IWebProxy webProxy, Uri uri)
        {
            var reply = webProxy.DownloadString(uri);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(reply);

            return htmlDoc;
        }

        public void SaveDocument(string path, HtmlDocument htmlDocument)
        {
            var indexPath = path + @"\index.html";
            if (!_fileSystem.Directory.Exists(path))
            {
                _fileSystem.Directory.CreateDirectory(path);
            }

            htmlDocument.Save(indexPath);
        }
    }
}