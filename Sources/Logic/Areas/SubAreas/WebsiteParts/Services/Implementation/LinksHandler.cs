using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.SubAreas.Files.Services;
using Mmu.Wds.Logic.Areas.SubAreas.UrlAlignment.Services;
using Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Models;

namespace Mmu.Wds.Logic.Areas.SubAreas.WebsiteParts.Services.Implementation
{
    internal class LinksHandler : PartHandlerBase
    {
        public LinksHandler(IFileRepository fileRepo, IUrlAlignmentService urlAligner, IFilePathFactory filePathFactory)
            : base(fileRepo, urlAligner, filePathFactory)
        {
        }

        protected override IReadOnlyCollection<WebsitePart> GetParts(HtmlDocument htmlDoc)
        {
            var unwantedLinks = new string[]
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