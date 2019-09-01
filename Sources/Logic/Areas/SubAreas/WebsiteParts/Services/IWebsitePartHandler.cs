using System;
using System.Net;
using HtmlAgilityPack;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services
{
    internal interface IWebsitePartHandler
    {
        void HandlePart(WebClient webClient,
                       HtmlDocument htmlDoc,
                       Uri downloadUri,
                       string targetPath);
    }
}