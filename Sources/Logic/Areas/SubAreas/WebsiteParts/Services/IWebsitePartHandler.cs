using System;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.SubAreas.WebCommunication.Services;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services
{
    internal interface IWebsitePartHandler
    {
        void HandlePart(IWebProxy webProxy,
                       HtmlDocument htmlDoc,
                       Uri downloadUri,
                       string targetPath);
    }
}