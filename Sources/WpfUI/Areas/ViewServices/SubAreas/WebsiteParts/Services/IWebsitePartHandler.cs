using System;
using HtmlAgilityPack;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebCommunication.Services;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Services
{
    public interface IWebsitePartHandler
    {
        void HandlePart(
            IWebProxy webProxy,
            HtmlDocument htmlDoc,
            Uri downloadUri,
            string targetPath,
            Action<InformationGridEntryViewData> onNewInfo);
    }
}