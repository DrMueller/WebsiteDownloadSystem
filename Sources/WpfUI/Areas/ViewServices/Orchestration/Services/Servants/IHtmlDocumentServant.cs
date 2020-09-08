using System;
using HtmlAgilityPack;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.Orchestration.Services.Servants
{
    public interface IHtmlDocumentServant
    {
        HtmlDocument CreateDocument(IWebProxy webProxy, Uri uri);

        void SaveDocument(string path, HtmlDocument htmlDocument);
    }
}