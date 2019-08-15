using System;
using System.Net;
using HtmlAgilityPack;

namespace Mmu.Wds.Logic.Areas.Services.WebsitePartHandler
{
    internal interface IWebsitePartHandler
    {
        void HandlePart(WebClient client,
                       HtmlDocument htmlDoc,
                       Uri downloadUri,
                       string targetPath);
    }
}