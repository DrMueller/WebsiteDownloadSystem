using System;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services;

namespace Mmu.Wds.Logic.Areas.Orchestration.Services.Servants
{
    internal interface IHtmlDocumentServant
    {
        HtmlDocument CreateDocument(IWebProxy webProxy, Uri uri);

        void SaveDocument(string path, HtmlDocument htmlDocument);
    }
}