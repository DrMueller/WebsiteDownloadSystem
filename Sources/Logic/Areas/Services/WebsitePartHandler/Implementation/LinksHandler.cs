using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Mmu.Wds.Logic.Areas.Services.Models;
using Mmu.Wds.Logic.Areas.Services.Servants;

namespace Mmu.Wds.Logic.Areas.Services.WebsitePartHandler.Implementation
{
    internal class LinksHandler : PartHandlerBase
    {
        public LinksHandler(IFilePathServant filePathServant, IUrlAlignmentServant urlAligner, IFilePathFactory filePathFactory)
            : base(filePathServant, urlAligner, filePathFactory)
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