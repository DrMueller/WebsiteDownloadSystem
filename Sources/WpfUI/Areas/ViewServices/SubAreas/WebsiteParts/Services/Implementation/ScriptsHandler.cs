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
    public class ScriptsHandler : PartHandlerBase
    {
        public ScriptsHandler(IFileRepository fileRepo, IUrlAlignmentService urlAligner, IFilePathFactory filePathFactory)
            : base(fileRepo, urlAligner, filePathFactory)
        {
        }

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc, Action<InformationGridEntryViewData> onNewInfo)
        {
            onNewInfo(new InformationGridEntryViewData("Fetching scripts.."));

            return htmlDoc.DocumentNode
                .Descendants()
                .Where(f => f.Name == "script")
                .Where(f => f.Attributes.Any(src => src.Name == "src"))
                .Select(f => f.Attributes.Single(src => src.Name == "src"))
                .Select(attr => new WebsitePart(attr))
                .ToList();
        }
    }
}