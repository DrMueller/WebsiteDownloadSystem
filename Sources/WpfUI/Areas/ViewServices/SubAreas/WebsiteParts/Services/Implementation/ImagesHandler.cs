using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.Files.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.UrlAlignment.Services;
using Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Models;

namespace Mmu.Wds.WpfUI.Areas.ViewServices.SubAreas.WebsiteParts.Services.Implementation
{
    public class ImagesHandler : PartHandlerBase
    {
        public ImagesHandler(
            IFileRepository filePathServant,
            IUrlAlignmentService urlAligner,
            IFilePathFactory filePathFactory)
            : base(filePathServant, urlAligner, filePathFactory)
        {
        }

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc, Action<InformationGridEntryViewData> onNewInfo)
        {
            onNewInfo(new InformationGridEntryViewData("Fetching images.."));

            return htmlDoc
                .DocumentNode
                .Descendants()
                .Where(f => f.Name == "img")
                .Select(f => f.Attributes.Single(f => f.Name == "src"))
                .Select(attr => new WebsitePart(attr))
                .ToList();
        }
    }
}