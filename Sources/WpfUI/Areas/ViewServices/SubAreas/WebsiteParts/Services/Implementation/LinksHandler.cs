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
    public class LinksHandler : PartHandlerBase
    {
        public LinksHandler(IFileRepository fileRepo, IUrlAlignmentService urlAligner, IFilePathFactory filePathFactory)
            : base(fileRepo, urlAligner, filePathFactory)
        {
        }

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc, Action<InformationGridEntryViewData> onNewInfo)
        {
            onNewInfo(new InformationGridEntryViewData("Fetching links.."));

            var unwantedLinks = new[]
            {
                "canonical",
                "alternate",
                "stylesheet" // Handled with CssFileHandler
            };

            return htmlDoc.DocumentNode
                .Descendants()
                .Where(f => f.Name == "link")
                .Where(f => !unwantedLinks.Contains(f.Attributes.Single(f => f.Name == "rel").Value))
                .Select(f => f.Attributes.Single(f => f.Name == "href"))
                .Select(attr => new WebsitePart(attr))
                .ToList();
        }
    }
}